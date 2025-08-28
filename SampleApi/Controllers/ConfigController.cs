using hoh.architecture.Shared.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace SampleApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigController : ControllerBase
    {
        private readonly IOptions<HohArchitectureOptions> _options;

        public ConfigController(IOptions<HohArchitectureOptions> options)
        {
            _options = options;
        }

        [HttpGet]
        [Route("GetOptions")]
        public HohArchitectureOptions GetOptions()
        {
            return _options.Value;
        }
    }
}
