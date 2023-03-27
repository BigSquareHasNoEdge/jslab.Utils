
using Jslab.Util.PassFilter.exams;

using Xunit;

namespace Jslab.Util.PassFilter.tests;

public class PipeLineTests
{
    [Fact]
    public void HappyPath_Run()
    {
        var stub = new Person( "ȫ�赿", 19);

        var actual = PassFilterExam.PipeLine.Run(stub);

        Assert.True(actual);
    }
}