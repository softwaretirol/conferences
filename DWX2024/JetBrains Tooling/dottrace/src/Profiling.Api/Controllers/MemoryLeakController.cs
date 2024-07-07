using Microsoft.AspNetCore.Mvc;
using Profiling.Api.Services.MemoryLeak;

namespace Profiling.Api.Controllers
{
    [ApiController]
    [Route("memory-leak")]
    public class MemoryLeakController : ControllerBase
    {
        private readonly IMemoryLeakService _service;
        public MemoryLeakController(IMemoryLeakService service)
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