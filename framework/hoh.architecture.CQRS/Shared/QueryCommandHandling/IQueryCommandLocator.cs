using hoh.architecture.CQRS.Query;

namespace hoh.architecture.CQRS.Shared.QueryCommandHandling;

public interface IQueryCommandLocator
{
    public Task<IQueryHandler<TQ, TR>> LocateQueryHandlerAsync<TQ, TR>() where TQ : IQuery where TR : class;

    //TODO JJH
    // public Task<TR> HandleCommandAsync<TC, TR>(TC command, IServiceProvider serviceProvider);

}