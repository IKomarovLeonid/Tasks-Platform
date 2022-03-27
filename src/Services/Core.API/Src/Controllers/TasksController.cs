using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Core.API.Src.Controllers
{
    [ApiController, Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync()
        {
            return Ok();
        }
    }
}
