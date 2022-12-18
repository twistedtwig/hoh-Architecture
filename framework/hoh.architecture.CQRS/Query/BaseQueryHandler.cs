using hoh.architecture.CQRS.Shared.Results;

namespace hoh.architecture.CQRS.Query
{
    public abstract class BaseQueryHandler<TQ, TR> : IQueryHandler<TQ, TR> where TQ : IQuery<TR>
    {
        public async Task<object> ExecuteAsync(IQuery qry)
        {
            return await ExecuteAsync((TQ)qry);
        }

        public abstract Task<IQueryResult<TR>> ExecuteAsync(TQ qry);
    }
}
