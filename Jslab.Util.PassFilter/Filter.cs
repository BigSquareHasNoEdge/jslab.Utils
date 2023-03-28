namespace Jslab.Util.PassFilter;

public abstract class Filter<T>
{
    protected Predicate<T>? NextTest;
    protected CancellationTokenSource? TokenSource;

    internal void SetNext(Predicate<T> nextTest)
        => NextTest = nextTest;
    internal void SetTokenSource(CancellationTokenSource tokenSource)
        => TokenSource = tokenSource;

    public bool Test(T model)
    {
        if (TokenSource != null&& TokenSource.IsCancellationRequested)
            return false;

        return TestIfNotCancelled(model);
    }

    protected abstract bool TestIfNotCancelled(T model);
}
