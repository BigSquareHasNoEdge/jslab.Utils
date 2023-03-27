namespace Jslab.Util.PassFilter.exams.PersonFilters;

class NameContainsKim : PostTester<Person>
{
    protected override bool PosttestIfNotCancelled(Person p)
    {
        return p.Name.Contains('김');
    }
}


