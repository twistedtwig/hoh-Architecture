using HoH.Architecture.CQRS.Command;
using HoH.Architecture.CQRS.Query;
using Microsoft.Extensions.DependencyInjection;

namespace HoH.Architecture.CQRS.Shared.QueryCommandHandling
{
    public class ServiceProviderQueryCommandLocator : IQueryCommandLocator
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceProviderQueryCommandLocator(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public Task<IQueryHandler<TQ, TR>> LocateQueryHandlerAsync<TQ, TR>() where TQ : IQuery where TR : class
        {
            var handler = _serviceProvider.GetRequiredService<IQueryHandler<TQ, TR>>();
            return Task.FromResult(handler);
        }

        public Task<ICommandHandler<TC>> LocateCommandHandlerAsync<TC>() where TC : ICommand
        {
            var handler = _serviceProvider.GetRequiredService<ICommandHandler<TC>>();
            return Task.FromResult(handler);
        }
    }
}
