using Chronicy.Data;
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
        // GET api/notebook/all
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Notebook>>> GetNotebooks()
        {
            return new List<Notebook>();
        }

        // GET api/notebook?id=5
        [HttpGet]
        public async Task<ActionResult<Notebook>> GetNotebook(long? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return new Notebook(id.ToString());
        }

        // POST api/notebook/create
        [HttpPost("create")]
        public async Task<ActionResult<Notebook>> CreateNotebook()
        {
            return null;
        }

        // DELETE api/notebook/delete?id=5
        [HttpDelete("delete")]
        public async Task DeleteNotebook(long id)
        {
            
        }

        // PUT api/notebook/update
        [HttpPut("update")]
        public async Task UpdateNotebook()
        {
            
        }
    }
}
