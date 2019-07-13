using Chronicy.Data;
using Chronicy.Web.Api;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chronicy.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotebookController : ControllerBase
    {
        private readonly INotebook notebooks;
        private readonly TokenManager tokenManager;

        public NotebookController(INotebook notebooks, TokenManager tokenManager)
        {
            this.notebooks = notebooks;
            this.tokenManager = tokenManager;
        }

        // GET api/notebook/all
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Notebook>>> GetNotebooks()
        {
            return new List<Notebook>(await notebooks.GetAllAsync());
        }

        // GET api/notebook?id=5
        [HttpGet]
        public async Task<ActionResult<Notebook>> GetNotebook(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await notebooks.GetAsync(id);
        }

        // POST api/notebook/create
        [HttpPost("create")]
        public async Task CreateNotebook()
        {
            // TODO: Notebook from body
            await notebooks.CreateAsync(null);
        }

        // DELETE api/notebook/delete?id=5
        [HttpDelete("delete")]
        public async Task DeleteNotebook(string id)
        {
            await notebooks.DeleteAsync(id);
        }

        // PUT api/notebook/update
        [HttpPut("update")]
        public async Task UpdateNotebook([FromBody] Notebook notebook)
        {
            await notebooks.UpdateAsync(notebook);
        }
    }
}
