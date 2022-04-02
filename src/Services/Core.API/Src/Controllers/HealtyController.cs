using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core.API.Src.Controllers
{
    [ApiController, Route("api"), AllowAnonymous]
    public class HealtyController : ControllerBase
    {
        [HttpGet("ping")]
        public ActionResult Ping()
        {
            return Ok("Pong");
        }
    }
}
