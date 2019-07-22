using Chronicy.Information;
using Chronicy.Web.Api;
using Chronicy.Web.Converters;
using Chronicy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chronicy.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class NotebookController : ControllerBase
    {
        private readonly INotebook notebooks;
        private readonly ITokenManager tokenManager;

        public NotebookController(INotebook notebooks, ITokenManager tokenManager)
        {
            this.notebooks = notebooks;
            this.tokenManager = tokenManager;
        }

        // GET api/notebook/all
        [HttpGet("all")]
        public async Task<ActionResult<ListResponse<Notebook>>> GetNotebooksAsync([FromHeader] string authorization)
        {
            if (authorization == null)
            {
                return ErrorResponse.Failure<ListResponse<Notebook>>(1, "Missing token");
            }

            if (!CheckToken(authorization))
            {
                return ErrorResponse.Failure<ListResponse<Notebook>>(1, "Invalid token");
            }

            List<Data.Notebook> list = new List<Data.Notebook>(await notebooks.GetAllAsync());
            return new ListResponse<Notebook> { List = list.ConvertAll((item) => item.ToWebNotebook()) };
        }

        // GET api/notebook?id=5
        [HttpGet]
        public async Task<ActionResult<Notebook>> GetNotebookAsync([FromHeader] string authorization, [FromQuery] int? id)
        {
            if (authorization == null)
            {
                return ErrorResponse.Failure<Notebook>(1, "Missing token");
            }

            if (!CheckToken(authorization))
            {
                return ErrorResponse.Failure<Notebook>(1, "Invalid token");
            }

            if (id == null)
            {
                return ErrorResponse.Failure<Notebook>(1, $"Parameter { nameof(id) } is missing");
            }

            return (await notebooks.GetAsync(id.Value)).ToWebNotebook();
        }

        // POST api/notebook/create
        [HttpPost("create")]
        public async Task<ActionResult<ErrorResponse>> CreateNotebookAsync([FromHeader] string authorization, [FromBody] Notebook notebook)
        {
            if (authorization == null)
            {
                return ErrorResponse.Failure(1, "Missing token");
            }

            if (!CheckToken(authorization))
            {
                return ErrorResponse.Failure(1, "Invalid token");
            }

            await notebooks.CreateAsync(notebook.ToDataNotebook());
            return ErrorResponse.Success();
        }

        // DELETE api/notebook/delete?id=5
        [HttpDelete("delete")]
        public async Task<ActionResult<ModelBase>> DeleteNotebookAsync([FromHeader] string authorization, [FromQuery] int? id)
        {
            if (authorization == null)
            {
                return ErrorResponse.Failure(1, "Missing token");
            }

            if (!CheckToken(authorization))
            {
                return ErrorResponse.Failure(1, "Invalid token");
            }

            if (id == null)
            {
                return ErrorResponse.Failure(1, $"Parameter { nameof(id) } is missing");
            }

            await notebooks.DeleteAsync(id.Value);
            return ErrorResponse.Success();
        }

        // PUT api/notebook/update
        [HttpPut("update")]
        public async Task<ActionResult<ErrorResponse>> UpdateNotebookAsync([FromHeader] string authorization, [FromBody] Notebook notebook)
        {
            if (authorization == null)
            {
                return ErrorResponse.Failure(1, "Missing token");
            }

            if (!CheckToken(authorization))
            {
                return ErrorResponse.Failure(1, "Invalid token");
            }

            if (notebook == null)
            {
                return ErrorResponse.Failure(1, $"The request body does not contain a valid Notebook");
            }

            await notebooks.UpdateAsync(notebook.ToDataNotebook());
            return ErrorResponse.Success();
        }

        private bool CheckToken(string token)
        {
            return tokenManager.GetTokenStatus(token) == TokenStatus.Valid;
        }
    }
}
