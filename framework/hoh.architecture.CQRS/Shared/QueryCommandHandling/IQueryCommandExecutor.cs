using HoH.Architecture.CQRS.Command;
using HoH.Architecture.CQRS.Query;
using HoH.Architecture.CQRS.Shared.Results;

namespace HoH.Architecture.CQRS.Shared.QueryCommandHandling
{
    public interface IQueryCommandExecutor
    {
        Task<IQueryResult<TR>> ExecuteQueryAsync<TQ, TR>(TQ query, CancellationToken cancellationToken = default) where TQ : IQuery where TR : class;

        Task<ICommandResult> ExecuteCommandAsync<TC>(TC command, CancellationToken cancellationToken = default) where TC : ICommand;
    }
}
