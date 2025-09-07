using HoH.Architecture.CQRS.Command;
using HoH.Architecture.CQRS.Query;

namespace HoH.Architecture.CQRS.Shared.QueryCommandHandling;

public interface IQueryCommandLocator
{
    public Task<IQueryHandler<TQ, TR>> LocateQueryHandlerAsync<TQ, TR>() where TQ : IQuery where TR : class;

    public Task<ICommandHandler<TC>> LocateCommandHandlerAsync<TC>() where TC : ICommand;

}