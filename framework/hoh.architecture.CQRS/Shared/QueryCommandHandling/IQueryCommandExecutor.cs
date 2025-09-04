using HoH.Architecture.CQRS.Command;
using HoH.Architecture.CQRS.Query;
using HoH.Architecture.CQRS.Shared.Results;

namespace HoH.Architecture.CQRS.Shared.QueryCommandHandling
{
    public interface IQueryCommandExecutor
    {
        Task<IQueryResult<TR>> ExecuteQueryAsync<TQ, TR>(TQ query) where TQ : IQuery where TR : class;
        Task<TR> ExecuteCommandAsync<TC, TR>(TC command) where TC : ICommand where TR : ICommandResult;
    }
}
