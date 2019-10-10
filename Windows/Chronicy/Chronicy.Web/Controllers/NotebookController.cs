using Chronicy.Web.Api;
using Chronicy.Web.Converters;
using Chronicy.Web.Models;
using Chronicy.Web.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Chronicy.Web.Controllers
{
    /// <summary>
    /// Provides access to stored notebooks.
    /// </summary>
    [Produces(MediaTypeNames.Application.Json)]
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
                return ErrorResponse.Failure<ListResponse<Notebook>>(ErrorCodes.MissingToken, "Missing token");
            }

            try
            {
                if (!await CheckTokenAsync(authorization))
                {
                    return ErrorResponse.Failure<ListResponse<Notebook>>(ErrorCodes.InvalidToken, "Invalid or expired token");
                }

                List<Data.Notebook> list = new List<Data.Notebook>(await notebooks.GetAllAsync());
                return new ListResponse<Notebook> { List = list.ConvertAll((item) => item.ToWebNotebook()) };
            }
            catch (Exception e)
            {
                return ErrorResponse.Failure<ListResponse<Notebook>>(ErrorCodes.GeneralFailure, e);
            }
        }

        // GET api/notebook?id=5
        [HttpGet]
        public async Task<ActionResult<Notebook>> GetNotebookAsync([FromHeader] string authorization, [FromQuery] int? id)
        {
            if (authorization == null)
            {
                return ErrorResponse.Failure<Notebook>(ErrorCodes.MissingToken, "Missing token");
            }

            try
            {
                if (!await CheckTokenAsync(authorization))
                {
                    return ErrorResponse.Failure<Notebook>(ErrorCodes.InvalidToken, "Invalid or expired token");
                }

                if (id == null)
                {
                    return ErrorResponse.Failure<Notebook>(ErrorCodes.MissingParameter, $"Parameter { nameof(id) } is missing");
                }

                return (await notebooks.GetAsync(id.Value)).ToWebNotebook();
            }
            catch (Exception e)
            {
                return ErrorResponse.Failure<Notebook>(ErrorCodes.GeneralFailure, e);
            }
        }

        // POST api/notebook/create
        [HttpPost("create")]
        public async Task<ActionResult<ErrorResponse>> CreateNotebookAsync([FromHeader] string authorization, [FromBody] Notebook notebook)
        {
            if (authorization == null)
            {
                return ErrorResponse.Failure(ErrorCodes.MissingToken, "Missing token");
            }

            try
            {
                if (!await CheckTokenAsync(authorization))
                {
                    return ErrorResponse.Failure(ErrorCodes.InvalidToken, "Invalid or expired token");
                }

                await notebooks.CreateAsync(notebook.ToDataNotebook());
            }
            catch (Exception e)
            {
                return ErrorResponse.Failure(ErrorCodes.GeneralFailure, e);
            }

            return ErrorResponse.Success();
        }

        // DELETE api/notebook/delete?id=5
        [HttpDelete("delete")]
        public async Task<ActionResult<ModelBase>> DeleteNotebookAsync([FromHeader] string authorization, [FromQuery] int? id)
        {
            if (authorization == null)
            {
                return ErrorResponse.Failure(ErrorCodes.MissingToken, "Missing token");
            }

            try
            {
                if (!await CheckTokenAsync(authorization))
                {
                    return ErrorResponse.Failure(ErrorCodes.InvalidToken, "Invalid or expired token");
                }

                if (id == null)
                {
                    return ErrorResponse.Failure(ErrorCodes.MissingParameter, $"Parameter { nameof(id) } is missing");
                }

                await notebooks.DeleteAsync(id.Value);
            }
            catch (Exception e)
            {
                return ErrorResponse.Failure(ErrorCodes.GeneralFailure, e);
            }

            return ErrorResponse.Success();
        }

        // PUT api/notebook/update
        [HttpPut("update")]
        public async Task<ActionResult<ErrorResponse>> UpdateNotebookAsync([FromHeader] string authorization, [FromBody] Notebook notebook)
        {
            if (authorization == null)
            {
                return ErrorResponse.Failure(ErrorCodes.MissingToken, "Missing token");
            }

            try
            {
                if (!await CheckTokenAsync(authorization))
                {
                    return ErrorResponse.Failure(ErrorCodes.InvalidToken, "Invalid or expired token");
                }

                if (notebook == null)
                {
                    return ErrorResponse.Failure(ErrorCodes.InvalidBody, $"The request body does not contain a valid Notebook");
                }

                await notebooks.UpdateAsync(notebook.ToDataNotebook());
            }
            catch (Exception e)
            {
                return ErrorResponse.Failure(ErrorCodes.GeneralFailure, e);
            }

            return ErrorResponse.Success();
        }

        private async Task<bool> CheckTokenAsync(string token)
        {
            return (await tokenManager.GetTokenStatusAsync(token)) == TokenStatus.Valid;
        }
    }
}
