
namespace Jslab.Util.PassFilter;

public class PassfilterPipeline<TContext>
{
    private readonly List<Type> _filters = new List<Type>();

    public CancellationTokenSource TokenSource { get; set; } = new CancellationTokenSource();

    Predicate<TContext> _predicate = DummyTest;
    public Predicate<TContext> Predicate => _predicate;

    public PassfilterPipeline<TContext> Add<TTester>() where TTester : Filter<TContext>
    {
        _filters.Add(typeof(TTester));
        return this;
    }

    public PassfilterPipeline<TContext> Remove<TTester>() where TTester : Filter<TContext>
    {
        var t = typeof(TTester);
        if(_filters.Contains(t))
            _filters.Remove(t);

        return Build();
    }

    public PassfilterPipeline<TContext> Build()
    {
        _predicate = Build(0);
        return this;
    }            

    private Predicate<TContext> Build(int filterIndex)
    {
        if (filterIndex < _filters.Count)
        {
            var nextTest = Build(filterIndex + 1);

            var filter = (Filter<TContext>)Activator.CreateInstance(_filters[filterIndex])!;

            filter.SetNext(nextTest);
            filter.SetTokenSource(TokenSource);

            return filter.Test;
        }
        else
        {
            return DummyTest;
        }
    }

    public bool Test(TContext context)
        => _predicate(context);

    private static bool DummyTest(TContext context)
        => true;

}
