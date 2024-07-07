using Microsoft.AspNetCore.Mvc;
using Profiling.Api.Services.BlockingThreads;

namespace Profiling.Api.Controllers
{
    [ApiController]
    [Route("blocking-threads")]
    public class BlockingThreadsController : ControllerBase
    {
        private readonly IBlockingThreadsService _service;
        public BlockingThreadsController(IBlockingThreadsService service)
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