using Jslab.Util.PassFilter.exams;
using Jslab.Util.PassFilter.exams.PersonFilters;

namespace Jslab.Util.PassFilter.tests;

public class FilterExam
{
    public static List<Person> People { get; } = new List<Person>()
{
    new Person("홍김동", 19),
    new Person("고김동", 16),
    new Person("홍길동", 19),
    new Person("고길동", 16),
};

    public static ConnectorOfFilter<Person> PipeLine { get; } = new ConnectorOfFilter<Person>()
            .Add<IsOver18>()
            .Add<NameContainsKim>()
            .Connect();

    public static void Run()
    {
        foreach (var p in People.FindAll(PipeLine.Predicate))
        {
            Console.WriteLine($"Name: {p.Name}, Age: {p.Age}");
        }

        var query = from p in People
                    where PipeLine.Test(p)
                    select p;

        foreach (var p in query)
        {
            Console.WriteLine($"Name: {p.Name}, Age: {p.Age}");
        }

        foreach (var p in People.Where( e => PipeLine.Test(e)))
        {
            Console.WriteLine($"Name: {p.Name}, Age: {p.Age}");
        }

        PipeLine.Remove<IsOver18>();
        Console.WriteLine("Removed");

        foreach (var p in People.FindAll(PipeLine.Predicate))
        {
            Console.WriteLine($"Name: {p.Name}, Age: {p.Age}");
        }

        Console.ReadKey();
    }
}


