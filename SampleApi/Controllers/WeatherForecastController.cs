using hoh.architecture.CQRS.Query;
using hoh.architecture.CQRS.Shared.Results;
using Microsoft.AspNetCore.Mvc;

namespace SampleApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IServiceProvider _serviceProvider;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var x = _serviceProvider.GetService(typeof(IQueryHandler<TestQuery, bool>));

            IQuery<bool> query = new TestQuery();
            
            var y = (IQueryHandler)x;
            var result = y.ExecuteAsync(query).Result;

            var r = (IQueryResult<bool>) result;

            var queryExecutor = _serviceProvider.GetService<IQueryExecutor>();

            var queryResult = queryExecutor.ExecuteAsync(query).Result;
            

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}