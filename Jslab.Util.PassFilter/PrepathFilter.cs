namespace Jslab.Util.PassFilter;

public abstract class PrepathFilter<T> : Filter<T>
{
    protected override bool TestIfNotCancelled(T model)
    {
        if (PretestIfNotCancelled(model))
            return NextTest != null && NextTest(model);

        return false;
    }

    protected abstract bool PretestIfNotCancelled(T model);
}
