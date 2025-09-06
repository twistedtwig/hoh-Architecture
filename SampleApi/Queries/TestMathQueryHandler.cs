
using HoH.Architecture.CQRS.Query;
using HoH.Architecture.CQRS.Shared.Results;

namespace SampleApi.Queries;

public class TestMathQueryHandler : IQueryHandler<TestMathQuery, TestMathQueryResult>
{
    public Task<IQueryResult<TestMathQueryResult>> ExecuteAsync(TestMathQuery query)
    {
        throw new NotImplementedException();
        var result = new TestMathQueryResult { Answer = query.First + query.Second };
        var queryResult = new QueryResult<TestMathQueryResult>(true, result);
        return Task.FromResult((IQueryResult<TestMathQueryResult>)queryResult);
    }
}