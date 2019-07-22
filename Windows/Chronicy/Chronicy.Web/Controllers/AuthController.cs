using Chronicy.Web.Api;
using Chronicy.Web.Models;
using Chronicy.Web.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Chronicy.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthentication authentication;

        public AuthController(IAuthentication authentication)
        {
            this.authentication = authentication;
        }

        // POST api/auth
        [HttpPost]
        public async Task<ActionResult<Token>> AuthenticateAsync([FromHeader] string authorization)
        {
            AuthorizationHeaderDecoder decoder = new AuthorizationHeaderDecoder(authorization);
            Tuple<string, string> usernamePassword = decoder.Decode();

            Token token = await authentication.AuthenticateAsync(usernamePassword.Item1, usernamePassword.Item2);
            return token;
        }
    }
}
