using Microsoft.AspNetCore.Mvc;
using Profiling.Api.Services.HighCpuUsage;

namespace Profiling.Api.Controllers
{
    [ApiController]
    [Route("high-cpu")]
    public class HighCpuUsageController : ControllerBase
    {
        private readonly IHighCpuUsageService _service;
        public HighCpuUsageController(IHighCpuUsageService service)
        {
            _service = service;
        }

        [HttpGet()]
        public IActionResult Get()
        {
           _service.Run();
           return Ok();
        }
    }
}