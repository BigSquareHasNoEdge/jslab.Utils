
namespace Jslab.Util.PassFilter;

public class ConnectorOfFilter<T>
{
    private readonly List<Type> _filters = new List<Type>();

    public CancellationTokenSource TokenSource { get; set; } = new CancellationTokenSource();

    Predicate<T> _predicate = DummyTest;
    public Predicate<T> Predicate => _predicate;

    public ConnectorOfFilter<T> Add<F>() where F : Filter<T>
    {
        _filters.Add(typeof(F));
        return this;
    }

    public ConnectorOfFilter<T> Remove<F>() where F : Filter<T>
    {
        var t = typeof(F);
        if(_filters.Contains(t))
            _filters.Remove(t);

        return Connect();
    }

    public ConnectorOfFilter<T> Connect()
    {
        _predicate = Connect(0);
        return this;
    }            

    private Predicate<T> Connect(int filterIndex)
    {
        if (filterIndex < _filters.Count)
        {
            var nextTest = Connect(filterIndex + 1);

            var filter = (Filter<T>)Activator.CreateInstance(_filters[filterIndex])!;

            filter.SetNext(nextTest);
            filter.SetTokenSource(TokenSource);

            return filter.Test;
        }
        else
        {
            return DummyTest;
        }
    }

    public bool Test(T context)
        => _predicate(context);

    private static bool DummyTest(T context)
        => true;

}
