using hoh.architecture.CQRS.Query;
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

        public QueryController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
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
    }
}