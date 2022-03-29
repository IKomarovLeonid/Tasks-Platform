using Core.API.Authentication;
using Core.API.View.Authentication;
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
        public ActionResult<AccessToken> GenerateToken([FromBody] TokenRequest request)
        {
            var context = new AuthenticationContext()
            {
                Username = request.Username,
                Password = request.Password,
            };


            var isAuthorized = _service.IsAuthenticated(context);

            if (!isAuthorized)
            {
                return Forbid();
            }

            var token = _service.GenerateToken(context);

            return new AccessToken()
            {
                Token = token
            };
        }

    }
}
