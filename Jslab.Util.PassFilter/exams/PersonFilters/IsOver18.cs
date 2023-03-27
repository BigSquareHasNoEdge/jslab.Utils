namespace Jslab.Util.PassFilter.exams.PersonFilters;

class IsOver18 : PreTester<Person>
{
    protected override bool PretestIfNotCancelled(Person p)
    {
        return p.Age > 18;
    }
}


