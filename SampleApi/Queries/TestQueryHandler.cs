using hoh.architecture.CQRS.Query;
using hoh.architecture.CQRS.Shared.Results;

namespace SampleApi.Queries
{
    public class TestQueryHandler : IQueryHandler<TestQuery, TestQueryResult>
    {
        public Task<IQueryResult<TestQueryResult>> ExecuteAsync(TestQuery qry)
        {
            var returnMessage = $"your message was: {qry.Message}";
            var result = new QueryResult<TestQueryResult>(true, new TestQueryResult{ Text = returnMessage});

            return Task.FromResult((IQueryResult<TestQueryResult>)result);
        }
    }
}
