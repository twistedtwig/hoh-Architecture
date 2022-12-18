using hoh.architecture.CQRS.Query;
using hoh.architecture.CQRS.Shared.Results;
using SampleApi;

namespace SampleApi
{
    public class TestQueryHandler : BaseQueryHandler<TestQuery, bool>
    {
        public override Task<IQueryResult<bool>> ExecuteAsync(TestQuery qry)
        {
            var result = new QueryResult<bool>(true, true);

            return Task.FromResult((IQueryResult<bool>)result);
        }
    }
}
