using hoh.architecture.CQRS.Command;
using hoh.architecture.CQRS.Query;
using hoh.architecture.CQRS.Shared.Results;

namespace hoh.architecture.CQRS.Shared.QueryCommandHandling
{
    public interface IQueryCommandExecutor
    {
        Task<IQueryResult<TR>> ExecuteAsync<TQ, TR>(TQ query) where TQ : IQuery where TR : class;
        Task<ICommandResult> ExecuteAsync<TC>(TC command) where TC : ICommand;
    }
}
