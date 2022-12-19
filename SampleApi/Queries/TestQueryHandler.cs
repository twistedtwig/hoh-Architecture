using hoh.architecture.CQRS.Query;
using hoh.architecture.CQRS.Shared.Results;

namespace SampleApi.Queries
{
    public class TestQueryHandler : IQueryHandler<TestQuery, string>
    {
        public Task<IQueryResult<string>> ExecuteAsync(TestQuery qry)
        {
            var returnMessage = $"your message was: {qry.Message}";
            var result = new QueryResult<string>(true, returnMessage);

            return Task.FromResult((IQueryResult<string>)result);
        }
    }
}
