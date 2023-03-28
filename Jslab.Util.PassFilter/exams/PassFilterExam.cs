using Jslab.Util.PassFilter.exams;
using Jslab.Util.PassFilter.exams.PersonFilters;

namespace Jslab.Util.PassFilter.tests;

public class PassFilterExam
{
    public static List<Person> People { get; } = new List<Person>()
{
    new Person("홍김동", 19),
    new Person("고김동", 16),
    new Person("홍길동", 19),
    new Person("고길동", 16),
};

    public static PassfilterPipeline<Person> PipeLine { get; } = new PassfilterPipeline<Person>()
            .Add<IsOver18>()
            .Add<NameContainsKim>()
            .Build();

    public static void Run()
    {
        foreach (var p in People.FindAll(PipeLine.Predicate))
        {
            Console.WriteLine($"Name: {p.Name}, Age: {p.Age}");
        }
        Console.WriteLine("Removed");

        PipeLine.Remove<IsOver18>();

        foreach (var p in People.FindAll(PipeLine.Predicate))
        {
            Console.WriteLine($"Name: {p.Name}, Age: {p.Age}");
        }

        Console.ReadKey();
    }
}


