using hoh.architecture.CQRS.Shared.Results;
using Microsoft.Extensions.DependencyInjection;

namespace hoh.architecture.CQRS.Query
{
    public abstract class RequestHandlerWrapperBase
    {
        public abstract Task<object?> HandleQuery(object request, IServiceProvider serviceProvider);

    }

    public abstract class QueryCreatorWrapper<TR> : RequestHandlerWrapperBase
    {
        public abstract Task<IQueryResult<TR>> HandleQuery(IQuery<TR> query, IServiceProvider serviceProvider);
    }

    public class QueryCreatorWrapperImpl<TQ, TR> : QueryCreatorWrapper<TR>
        where TQ : IQuery<TR>
    {
        public override async Task<object?> HandleQuery(object query, IServiceProvider serviceProvider) =>
            await HandleQuery((IQuery<TR>)query, serviceProvider).ConfigureAwait(false);

        public override Task<IQueryResult<TR>> HandleQuery(IQuery<TR> query, IServiceProvider serviceProvider)
        {
            var handler = serviceProvider.GetRequiredService<IQueryHandler<TQ, TR>>();

            return handler.ExecuteAsync((TQ)query);
        }
    }
}
