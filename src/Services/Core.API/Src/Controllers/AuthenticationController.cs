using Core.API.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core.API.Controllers
{
    [ApiController, Route("api/auth"), AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthentication _service;
        public AuthenticationController(IAuthentication service)
        {
            _service = service;
        }

        [HttpPost("generate")]
        public ActionResult GenerateToken(string username, string password)
        {
            var isAuthorized = _service.IsAuthenticated(new AuthenticationContext()
            {
                Username = username,
                Password = password
            });

            if (!isAuthorized)
            {
                return Forbid();
            }

            return Ok();
        }
    }
}
