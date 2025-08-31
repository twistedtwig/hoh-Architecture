using HoH.Architecture.CQRS.Shared.Results;

namespace HoH.Architecture.CQRS.Query
{
    public interface IQueryHandler<in TQ, TR> where TQ : IQuery where TR : class
    {
        Task<IQueryResult<TR>> ExecuteAsync(TQ query);
    }
}
