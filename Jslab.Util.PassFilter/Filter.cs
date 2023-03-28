using System;
using System.Threading;

namespace Jslab.Util.PassFilter;

public abstract class Filter<TContext>
{
    protected Predicate<TContext>? NextTest;
    protected CancellationTokenSource? TokenSource;       
    

    internal void SetNext(Predicate<TContext> nextTest)
        => NextTest = nextTest;
    internal void SetTokenSource(CancellationTokenSource tokenSource)
        => TokenSource = tokenSource;

    public bool Test(TContext context)
    {
        if (TokenSource != null&& TokenSource.IsCancellationRequested)
            return false;

        return TestIfNotCancelled(context);
    }

    protected abstract bool TestIfNotCancelled(TContext context);
}

public abstract class PreTester<TContext> : Filter<TContext>
{
    protected override bool TestIfNotCancelled(TContext context)
    {
        if (PretestIfNotCancelled(context))
            return NextTest != null && NextTest(context);

        return false;
    }

    protected abstract bool PretestIfNotCancelled(TContext context);
}

public abstract class PostTester<TContext> : Filter<TContext>
{
    protected override bool TestIfNotCancelled(TContext context)
    {
        if (NextTest != null && NextTest(context))
            return PosttestIfNotCancelled(context);

        return false;
    }

    protected abstract bool PosttestIfNotCancelled(TContext context);
}
