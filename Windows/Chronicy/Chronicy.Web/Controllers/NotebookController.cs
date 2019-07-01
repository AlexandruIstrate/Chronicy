using Chronicy.Data;
using Chronicy.Data.Storage;
using Chronicy.Sql;
using Chronicy.Web.Auth;
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
        private IDataSource dataSource;
        private TokenManager tokenManager;

        public NotebookController()
        {
            dataSource = new SqlDataSource();
            tokenManager = new TokenManager();
        }

        // GET api/notebook/all
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Notebook>>> GetNotebooks()
        {
            return new List<Notebook>(await dataSource.GetNotebooksAsync());
        }

        // GET api/notebook?id=5
        [HttpGet]
        public async Task<ActionResult<Notebook>> GetNotebook(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await dataSource.GetNotebookAsync(id);
        }

        // POST api/notebook/create
        [HttpPost("create")]
        public async Task<ActionResult<Notebook>> CreateNotebook()
        {
            return await dataSource.CreateNotebookAsync("Name");
        }

        // DELETE api/notebook/delete?id=5
        [HttpDelete("delete")]
        public async Task DeleteNotebook(string id)
        {
            await dataSource.DeleteNotebookAsync(await dataSource.GetNotebookAsync(id));
        }

        // PUT api/notebook/update
        [HttpPut("update")]
        public async Task UpdateNotebook([FromBody] Notebook notebook)
        {
            await dataSource.UpdateNotebookAsync(notebook);
        }
    }
}
