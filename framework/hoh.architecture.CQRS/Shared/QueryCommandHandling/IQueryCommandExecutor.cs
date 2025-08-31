using HoH.Architecture.CQRS.Command;
using HoH.Architecture.CQRS.Query;
using HoH.Architecture.CQRS.Shared.Results;

namespace HoH.Architecture.CQRS.Shared.QueryCommandHandling
{
    public interface IQueryCommandExecutor
    {
        Task<IQueryResult<TR>> ExecuteAsync<TQ, TR>(TQ query) where TQ : IQuery where TR : class;
        Task<ICommandResult> ExecuteAsync<TC>(TC command) where TC : ICommand;
    }
}
