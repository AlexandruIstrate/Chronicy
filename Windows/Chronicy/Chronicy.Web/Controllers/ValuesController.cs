using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Chronicy.Sql;
using Microsoft.AspNetCore.Mvc;

namespace ChronicyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly SqlServerDatabase database;

        public ValuesController(SqlServerDatabase database)
        {
            this.database = database;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            try
            {
                int affected = database.RunNonQueryProcedure("dbo.UserCreate", new List<SqlParameter>
                {
                    new SqlParameter("@username", "test3"),
                    new SqlParameter("@email", "test@test3.com"),
                    new SqlParameter("@phone", "911"),
                    new SqlParameter("@password", "test123")
                });

                return "Rows affected: " + affected.ToString();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
