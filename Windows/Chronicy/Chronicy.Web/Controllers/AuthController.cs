using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chronicy.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // POST api/auth
        [HttpGet]
        public async Task<ActionResult<string>> GetToken()
        {
            // TODO: Retrieve username and password from body
            IHeaderDictionary headers = HttpContext.Request.Headers;
            Dictionary<string, string[]> items = new Dictionary<string, string[]>();

            foreach (KeyValuePair<string, StringValues> header in headers)
            {
                items.Add(header.Key, header.Value.ToArray());
            }

            return items.ToString();

            //return string.Empty;
        }
    }
}
