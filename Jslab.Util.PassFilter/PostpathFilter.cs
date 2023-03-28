namespace Jslab.Util.PassFilter;

public abstract class PostpathFilter<T> : Filter<T>
{
    protected override bool TestIfNotCancelled(T model)
    {
        if (NextTest != null && NextTest(model))
            return PosttestIfNotCancelled(model);

        return false;
    }

    protected abstract bool PosttestIfNotCancelled(T model);
}
