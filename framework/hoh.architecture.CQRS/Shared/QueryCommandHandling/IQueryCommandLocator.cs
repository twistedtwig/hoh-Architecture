using hoh.architecture.CQRS.Command;
using hoh.architecture.CQRS.Query;
using hoh.architecture.CQRS.Shared.Results;

namespace hoh.architecture.CQRS.Shared.QueryCommandHandling;

public interface IQueryCommandLocator
{
    public Task<IQueryHandler<TQ, TR>> LocateQueryHandlerAsync<TQ, TR>() where TQ : IQuery where TR : class;

    public Task<ICommandHandler<TC, TR>> LocateCommandHandlerAsync<TC, TR>(TC command) where TC : ICommand where TR : ICommandResult;

}