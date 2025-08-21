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
        private readonly IQueryExecutor _executor;

        public QueryController(
            IServiceProvider serviceProvider,
            IQueryCommandLocator locator,
            IQueryHandler<TestQuery, TestQueryResult> queryHandler,
            IQueryExecutor executor)
        {
            _serviceProvider = serviceProvider;
            _locator = locator;
            _queryHandler = queryHandler;
            _executor = executor;
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

        [HttpGet]
        [Route("GetTestQueryExecutor")]
        public async Task<IQueryResult<TestQueryResult>> GetExecutorTestQuery()
        {
            var query = new TestQuery("This is my message4");

            var queryResult = await _executor.ExecuteAsync<TestQuery, TestQueryResult>(query);

            return queryResult;
        }

        [HttpPost]
        [Route("math")]
        public async Task<IQueryResult<TestMathQueryResult>> GetMathQuery(TestMathQuery query)
        {
            var queryResult = await _executor.ExecuteAsync<TestMathQuery, TestMathQueryResult>(query);
            return queryResult;
        }
    }
}