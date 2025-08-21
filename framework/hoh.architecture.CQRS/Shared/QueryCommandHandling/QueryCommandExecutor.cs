using hoh.architecture.CQRS.Command;
using hoh.architecture.CQRS.Query;
using hoh.architecture.CQRS.Shared.Results;

namespace hoh.architecture.CQRS.Shared.QueryCommandHandling
{
    public class QueryCommandExecutor : IQueryCommandExecutor
    {
        private readonly IQueryCommandLocator _queryCommandLocator;

        public QueryCommandExecutor(IQueryCommandLocator queryCommandLocator)
        {
            _queryCommandLocator = queryCommandLocator;
        }

        public async Task<IQueryResult<TR>> ExecuteAsync<TQ, TR>(TQ query) where TQ : IQuery where TR : class
        {
            /*
             * locate query handler
             * optional logging of query and execution duration and time.
             * saving query if required
             */

            // if (query == null)
            // {
            //     throw new ArgumentNullException(nameof(query));
            // }

            var queryHandler = await _queryCommandLocator.LocateQueryHandlerAsync<TQ, TR>();

            var result = await queryHandler.ExecuteAsync(query);

            // var queryType = query.GetType();
            // var handler = (RequestCreatorWrapper<T>)Activator.CreateInstance(typeof(RequestCreatorWrapperImpl<,>).MakeGenericType(queryType, typeof(T)));
            //
            // if (handler == null)
            // {
            //     throw new ArgumentNullException(nameof(handler));
            //
            // }
            //
            // //TODO logging etc
            //
            // var result = await _queryCommandRequestHandler.HandleQueryAsync<>().HandleQuery(query, _serviceProvider);

            //TODO more logging

            return result;
        }

        public Task<ICommandResult> ExecuteAsync<TC>(TC command) where TC : ICommand
        {
            throw new NotImplementedException();
        }
    }
}
