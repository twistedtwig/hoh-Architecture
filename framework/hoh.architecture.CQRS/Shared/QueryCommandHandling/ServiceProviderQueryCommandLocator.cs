using hoh.architecture.CQRS.Query;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace hoh.architecture.CQRS.Shared.QueryCommandHandling
{
    // public abstract class RequestHandlerWrapperBase
    // {
    //     public abstract Task<object?> HandleQueryAsync(object request, IServiceProvider serviceProvider);
    //
    //     public abstract Task<object?> HandleCommandAsync(object request, IServiceProvider serviceProvider);
    //
    // }

    // public abstract class RequestCreatorWrapper<TR> : RequestHandlerWrapperBase
    // {
    //     public abstract Task<IQueryResult<TR>> HandleQueryAsync(IQuery<TR> query, IServiceProvider serviceProvider);
    // }

    public class ServiceProviderQueryCommandLocator : IQueryCommandLocator
    {
        private readonly IServiceProvider _serviceProvider;

        // public async Task<object?> HandleQueryAsync(object query, IServiceProvider serviceProvider) =>
        //     await HandleQueryAsync((IQuery<TR>)query, serviceProvider).ConfigureAwait(false);

        public ServiceProviderQueryCommandLocator(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        // public async Task<IQueryResult<TR>> HandleQueryAsync<TQ, TR>(IQuery<TQ> query) where TQ : IQuery<TR>
        // {
        //     var handler = _serviceProvider.GetRequiredService<IQueryHandler<TQ, TR>>();
        //
        //     return await handler.ExecuteAsync(query);
        // }

        // public Task<object?> HandleCommandAsync(object command, IServiceProvider serviceProvider) =>
        //     await HandleCommand((ICommand)command, serviceProvider).ConfigureAwait(false);
        //
        // public async Task<IQueryResult<TR>> HandleCommand(ICommand command, IServiceProvider serviceProvider)
        // {
        //     var handler = serviceProvider.GetRequiredService<IQueryHandler<TQ, TR>>();
        //
        //     return handler.ExecuteAsync((TQ)query);
        // }

        // public Task<IQueryHandler<TQ, TR>> LocateQueryHandlerAsync<TQ, TR>(IQuery<TQ> query) where TQ : IQuery<TR>
        // {
        //     var handler = _serviceProvider.GetRequiredService<IQueryHandler<TQ, TR>>();
        //     return Task.FromResult(handler);
        // }

        public Task<IQueryHandler<TQ, TR>> LocateQueryHandlerAsync<TQ, TR>() where TQ : IQuery where TR : class
        {
            var handler = _serviceProvider.GetRequiredService<IQueryHandler<TQ, TR>>();
            return Task.FromResult(handler);
        }
    }
}
