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
        private readonly ServiceProviderQueryCommandLocator _locator;

        public QueryController(IServiceProvider serviceProvider, ServiceProviderQueryCommandLocator locator)
        {
            _serviceProvider = serviceProvider;
            _locator = locator;
        }
        
        [HttpGet]
        [Route("GetTestQuery")]
        public async Task<IQueryResult<string>> GetTestQuery()
        {
            var query = new TestQuery("This is my message");
        
            var queryExecutor = _serviceProvider.GetService<IQueryExecutor>();
        
            var queryResult = await queryExecutor.ExecuteAsync(query);
        
            return queryResult;
        }

        [HttpGet]
        [Route("GetTestQuery")]
        public async Task<IQueryResult<string>> GetInjectedTestQuery()
        {
            var query = new TestQuery("This is my message");

            var queryExecutor = _serviceProvider.GetService<IQueryExecutor>();

            var queryResult = await queryExecutor.ExecuteAsync(query);

            return queryResult;
        }
    }
}