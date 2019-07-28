using Chronicy.Web.Api;
using Chronicy.Web.Models;
using Chronicy.Web.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Chronicy.Web.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
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
            if (authorization == null)
            {
                return ErrorResponse.Failure<Token>(ErrorCodes.MissingCredentials, "Missing username or password");
            }

            try
            {
                AuthorizationHeaderDecoder decoder = new AuthorizationHeaderDecoder(authorization);
                Tuple<string, string> usernamePassword = decoder.Decode();

                Token token = await authentication.AuthenticateAsync(usernamePassword.Item1, usernamePassword.Item2);
                return token;
            }
            catch (Exception e)
            {
                return ErrorResponse.Failure<Token>(ErrorCodes.GeneralFailure, e.Message);
            }
        }
    }
}
