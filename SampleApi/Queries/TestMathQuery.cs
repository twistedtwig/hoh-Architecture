
using HoH.Architecture.CQRS.Query;

namespace SampleApi.Queries;

public class TestMathQuery : IQuery
{
    public TestMathQuery(int first, int second)
    {
        First = first;
        Second = second;
    }
    public int First { get; set; }
    public int Second { get; set; }
}