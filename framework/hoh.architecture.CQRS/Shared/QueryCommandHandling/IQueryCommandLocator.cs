using hoh.architecture.CQRS.Query;
using hoh.architecture.CQRS.Shared.Results;

namespace hoh.architecture.CQRS.Shared.QueryCommandHandling;

public interface IQueryCommandLocator
{
    public Task<IQueryHandler<TQ, TR>> LocateQueryHandlerAsync<TQ, TR>() where TQ : IQuery where TR : IQueryResult<TR>;

    //TODO JJH
    // public Task<TR> HandleCommandAsync<TC, TR>(TC command, IServiceProvider serviceProvider);

}