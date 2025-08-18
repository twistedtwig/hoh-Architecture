using hoh.architecture.CQRS.Shared.Results;

namespace hoh.architecture.CQRS.Query
{
    public interface IQueryExecutor
    {
        Task<IQueryResult<TR>> ExecuteAsync<TQ, TR>(TQ query) where TQ : IQuery where TR : IQueryResult<TR>;
    }
}
