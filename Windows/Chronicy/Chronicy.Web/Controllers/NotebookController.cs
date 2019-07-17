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
        private readonly ITokenManager tokenManager;

        public NotebookController(INotebook notebooks, ITokenManager tokenManager)
        {
            this.notebooks = notebooks;
            this.tokenManager = tokenManager;
        }

        // GET api/notebook/all
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Notebook>>> GetNotebooks()
        {
            // TODO!!!: Make a wrapper with a root node named "Items"

            return new List<Notebook> {
                new Notebook
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
                }
            };

            //return new List<Notebook>(await notebooks.GetAllAsync());
        }

        // GET api/notebook?id=5
        [HttpGet]
        public async Task<ActionResult<Notebook>> GetNotebook(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await notebooks.GetAsync(id.Value);
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
        public async Task DeleteNotebook(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            await notebooks.DeleteAsync(id.Value);
        }

        // PUT api/notebook/update
        [HttpPut("update")]
        public async Task UpdateNotebook([FromBody] Notebook notebook)
        {
            if (notebook == null)
            {
                throw new ArgumentNullException(nameof(notebook));
            }

            await notebooks.UpdateAsync(notebook);
        }
    }
}
