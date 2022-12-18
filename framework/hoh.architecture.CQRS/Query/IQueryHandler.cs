using hoh.architecture.CQRS.Shared.Results;

namespace hoh.architecture.CQRS.Query
{
    public interface IQueryHandler
    {
        Task<object> ExecuteAsync(IQuery qry);
    }

    public interface IQueryHandler<in TQ, TR> : IQueryHandler
        where TQ : IQuery<TR>
    {
        Task<IQueryResult<TR>> ExecuteAsync(TQ query);
    }
}
