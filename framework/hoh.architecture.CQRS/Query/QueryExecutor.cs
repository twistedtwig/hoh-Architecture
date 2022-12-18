using hoh.architecture.CQRS.Shared.Results;
using Microsoft.Extensions.DependencyInjection;

namespace hoh.architecture.CQRS.Query
{
    public class QueryExecutor : IQueryExecutor
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryExecutor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<IQueryResult<T>> ExecuteAsync<T>(IQuery<T> query)
        {
            /*
             * locate query handler
             * optional logging of query and execution duration and time.
             * saving query if required
             */

            var handlerType = ConvertToQueryHandlerType<T>(query);
            if (_serviceProvider.GetService(handlerType) is not IQueryHandler handler)
            {
                var message = $"No Handler for query type '{query.GetType().Name}'";
                return new QueryResult<T>(false, default(T), new ExceptionalMessage(message));
            }


            //TODO logging etc
            
            var rawResult = await handler.ExecuteAsync(query);

            //TODO more logging

            return (IQueryResult<T>) rawResult;
        }

        private Type ConvertToQueryHandlerType<T>(IQuery<T> qry)
        {
            var handlerType = typeof(IQueryHandler<,>);
            var constructedHandler = handlerType.MakeGenericType(qry.GetType(), typeof(T));

            return constructedHandler;
        }
    }
}
