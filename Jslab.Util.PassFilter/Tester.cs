using System;
using System.Threading;

namespace Jslab.Util.PassFilter;

public interface ILinkedTester<TContext>
{
    bool Test(TContext context);
    
    ILinkedTester<TContext> NextTester { get; set; }
    Predicate<TContext> NextTest { get; set; }
}

public abstract class ForwardTest<TContext> : ILinkedTester<TContext>
{
    public ILinkedTester<TContext> NextTester { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public Predicate<TContext> NextTest { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public bool Test(TContext context)
    {
        throw new NotImplementedException();
    }
}

public abstract class Tester<TContext>
{
    protected Predicate<TContext>? NextTest;
    protected CancellationTokenSource? TokenSource;       
    

    internal void SetNext(Predicate<TContext> nextTest)
        => NextTest = nextTest;
    internal void SetTokenSource(CancellationTokenSource tokenSource)
        => TokenSource = tokenSource;

    public bool Test(TContext context)
    {
        if (TokenSource != null && TokenSource.IsCancellationRequested)
            return false;
        
        return TestIfNotCancelled(context);
    }

    protected abstract bool TestIfNotCancelled(TContext context);
}

public abstract class PreTester<TContext> : Tester<TContext>
{
    protected override bool TestIfNotCancelled(TContext context)
    {
        if (PretestIfNotCancelled(context))
            return NextTest != null && NextTest(context);

        return false;
    }

    protected abstract bool PretestIfNotCancelled(TContext context);
}

public abstract class PostTester<TContext> : Tester<TContext>
{
    protected override bool TestIfNotCancelled(TContext context)
    {
        if (NextTest != null && NextTest(context))
            return PosttestIfNotCancelled(context);

        return false;
    }

    protected abstract bool PosttestIfNotCancelled(TContext context);
}
