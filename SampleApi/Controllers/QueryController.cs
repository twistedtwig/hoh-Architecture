using hoh.architecture.CQRS.Query;
using hoh.architecture.CQRS.Shared.QueryCommandHandling;
using hoh.architecture.CQRS.Shared.Results;
using Microsoft.AspNetCore.Mvc;
using SampleApi.Queries;

namespace SampleApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QueryController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IQueryCommandLocator _locator;
        private readonly IQueryHandler<TestQuery, TestQueryResult> _queryHandler;

        public QueryController(IServiceProvider serviceProvider, IQueryCommandLocator locator, IQueryHandler<TestQuery, TestQueryResult> queryHandler)
        {
            _serviceProvider = serviceProvider;
            _locator = locator;
            _queryHandler = queryHandler;
        }

        [HttpGet]
        [Route("GetTestQueryDirectInjection")]
        public async Task<IQueryResult<TestQueryResult>> GetTestQueryManual()
        {
            var query = new TestQuery("This is my message1");

            var queryResult = await _queryHandler.ExecuteAsync(query);

            return queryResult;
        }

        [HttpGet]
        [Route("GetTestQuery")]
        public async Task<IQueryResult<TestQueryResult>> GetTestQuery()
        {
            var query = new TestQuery("This is my message2");
        
            var queryHandler = _serviceProvider.GetService<IQueryHandler<TestQuery, TestQueryResult>>();
        
            var queryResult = await queryHandler.ExecuteAsync(query);
        
            return queryResult;
        }

        [HttpGet]
        [Route("GetTestQueryInjected")]
        public async Task<IQueryResult<TestQueryResult>> GetInjectedTestQuery()
        {
            var query = new TestQuery("This is my message3");

            var queryHandler = await _locator.LocateQueryHandlerAsync<TestQuery, TestQueryResult>();

            var queryResult = await queryHandler.ExecuteAsync(query);

            return queryResult;
        }
    }
}