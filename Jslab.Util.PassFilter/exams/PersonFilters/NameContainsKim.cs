namespace Jslab.Util.PassFilter.exams.PersonFilters;

class NameContainsKim : PostpathFilter<Person>
{
    protected override bool PosttestIfNotCancelled(Person p)
    {
        return p.Name.Contains('김');
    }
}


