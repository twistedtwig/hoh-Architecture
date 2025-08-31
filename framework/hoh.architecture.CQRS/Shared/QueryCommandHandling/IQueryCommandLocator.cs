using HoH.Architecture.CQRS.Command;
using HoH.Architecture.CQRS.Query;
using HoH.Architecture.CQRS.Shared.Results;

namespace HoH.Architecture.CQRS.Shared.QueryCommandHandling;

public interface IQueryCommandLocator
{
    public Task<IQueryHandler<TQ, TR>> LocateQueryHandlerAsync<TQ, TR>() where TQ : IQuery where TR : class;

    public Task<ICommandHandler<TC, TR>> LocateCommandHandlerAsync<TC, TR>(TC command) where TC : ICommand where TR : ICommandResult;

}