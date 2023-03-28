
using Jslab.Util.PassFilter.exams;

using Xunit;

namespace Jslab.Util.PassFilter.tests;

public class PipeLineTests
{
    [Fact]
    public void HappyPath_Run()
    {
        var stub = new Person( "ȫ�赿", 19);

        var actual = FilterExam.PipeLine.Test(stub);

        Assert.True(actual);
    }
}