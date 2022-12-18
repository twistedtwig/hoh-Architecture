using hoh.architecture.CQRS.Shared.Results;

namespace hoh.architecture.CQRS.Query
{
    public interface IQueryExecutor
    {
        Task<IQueryResult<T>> ExecuteAsync<T>(IQuery<T> query);
    }
}
