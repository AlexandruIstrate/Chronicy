using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Chronicy.Data;
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
                DataSet dataSet = database.RunScalarProcedure(SqlProcedures.User.Read, new List<SqlParameter>
                {
                    new SqlParameter("@username", "%"),
                    new SqlParameter("@email", "%"),
                    new SqlParameter("@phone", "%")
                });

                DataTable dataTable = dataSet.Tables[0];

                StringBuilder stringBuilder = new StringBuilder();

                foreach (DataRow row in dataTable.Rows)
                {
                    stringBuilder.AppendLine("----- Record -----");

                    foreach (DataColumn column in dataTable.Columns)
                    {
                        stringBuilder.Append(column.ColumnName);
                        stringBuilder.Append(" : ");
                        stringBuilder.AppendLine(row[column].ToString());
                    }
                }

                return stringBuilder.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            try
            {
                DataSet dataSet = database.RunScalarProcedure(SqlProcedures.User.Read, new List<SqlParameter>
                {
                    new SqlParameter("@username", "%"),
                    new SqlParameter("@email", "%"),
                    new SqlParameter("@phone", "%")
                });

                DataTable dataTable = dataSet.Tables[0];
                DataRow row = dataTable.Rows[0];

                return string.Empty;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("notebook")]
        public ActionResult<Notebook> GetNotebook()
        {
            return new Notebook
            {
                Name = "Notebook1",
                Stacks = new List<Stack>
                {
                    new Stack
                    {
                        Name = "Stack1",
                        Fields = new List<CustomField>
                        {
                            new CustomField { Name = "Field1", Type = FieldType.String },
                            new CustomField { Name = "Field2", Type = FieldType.Number }
                        },
                        Cards = new List<Card>
                        {
                            new Card
                            {
                                Name = "Card1",
                                Comment = "Comment1",
                                Fields = new List<CustomField>
                                {
                                    new CustomField { Name = "Field1", Type = FieldType.String, Value = "Hello, world!" },
                                    new CustomField { Name = "Field2", Type = FieldType.Number, Value = 1776 }
                                },
                                Tags = new List<Tag>
                                {
                                    new Tag { Name = "Tag1", Description = "Description1" }
                                }
                            }
                        }
                    },
                    new Stack
                    {
                        Name = "Stack2",
                        Fields = new List<CustomField>
                        {
                            new CustomField { Name = "Field2", Type = FieldType.Number },
                            new CustomField { Name = "Field3", Type = FieldType.Number },
                            new CustomField { Name = "Field5", Type = FieldType.String }
                        }
                    }
                }
            };
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
