using hoh.architecture.CQRS.Shared.Results;

namespace hoh.architecture.CQRS.Query
{
    public interface IQueryHandler<in TQ, TR> where TQ : IQuery where TR : IQueryResult<TR>
    {
        Task<TR> ExecuteAsync(TQ query);
    }
}
