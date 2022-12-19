using hoh.architecture.CQRS.Shared.Results;

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

            
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var queryType = query.GetType();
            var handler = (QueryCreatorWrapper<T>)Activator.CreateInstance(typeof(QueryCreatorWrapperImpl<,>).MakeGenericType(queryType, typeof(T)));

            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));

            }
           
            //TODO logging etc

            var result = await handler.HandleQuery(query, _serviceProvider);

            //TODO more logging

            return result;
        }
    }
}
