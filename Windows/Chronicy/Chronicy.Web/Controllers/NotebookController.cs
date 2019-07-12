using Chronicy.Data;
using Chronicy.Data.Storage;
using Chronicy.Sql;
using Chronicy.Web.Auth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Chronicy.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotebookController : ControllerBase
    {
        private IDataSource<Notebook> dataSource;
        private TokenManager tokenManager;

        public NotebookController(SqlConnection connection)
        {
            dataSource = new SqlDataSource(connection);
            tokenManager = new TokenManager();
        }

        // GET api/notebook/all
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Notebook>>> GetNotebooks()
        {
            return new List<Notebook>(await dataSource.GetAllAsync());
        }

        // GET api/notebook?id=5
        [HttpGet]
        public async Task<ActionResult<Notebook>> GetNotebook(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await dataSource.GetAsync(id);
        }

        // POST api/notebook/create
        [HttpPost("create")]
        public async Task CreateNotebook()
        {
            // TODO: Notebook from body
            await dataSource.CreateAsync(null);
        }

        // DELETE api/notebook/delete?id=5
        [HttpDelete("delete")]
        public async Task DeleteNotebook(string id)
        {
            await dataSource.DeleteAsync(id);
        }

        // PUT api/notebook/update
        [HttpPut("update")]
        public async Task UpdateNotebook([FromBody] Notebook notebook)
        {
            await dataSource.UpdateAsync(notebook);
        }
    }
}
