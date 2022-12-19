using hoh.architecture.CQRS.Shared.Results;

namespace hoh.architecture.CQRS.Query
{
    public interface IQueryHandler<in TQ, TR> where TQ : IQuery<TR>
    {
        Task<IQueryResult<TR>> ExecuteAsync(TQ query);
    }
}
