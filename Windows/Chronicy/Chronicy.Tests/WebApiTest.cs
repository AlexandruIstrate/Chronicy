using Chronicy.Web;
using Chronicy.Web.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace Chronicy.Tests
{
    [TestFixture]
    public class WebApiTest
    {
        private ChronicyWebApi webApi = new ChronicyWebApi("");

        [Test]
        public void CanAuthenticate()
        {
            const string username = "Fill this in";
            const string password = "Fill this in";

            Token token = null;

            Assert.DoesNotThrow(() => { token = webApi.Authenticate(username, password); }, "The client API must handle the request successfully!");
            Assert.IsNotNull(token, "The token returned must not be null!");
        }

        [Test]
        public void CanGetNotebooks()
        {
            ListResponse<Notebook> notebooks = null;

            Assert.DoesNotThrow(() => { notebooks = webApi.GetNotebooks(); }, "The client API must handle the request successfully!");
            Assert.IsNotNull(notebooks, "The list of notebooks returned must not be null!");
        }

        [Test]
        public void CanGetNotebook()
        {
            int id = 0;

            Notebook notebook = null;

            Assert.DoesNotThrow(() => { notebook = webApi.GetNotebook(id); }, "The client API must handle the request successfully!");
            Assert.IsNotNull(notebook, "The notebook returned must not be null!");
        }

        [Test]
        public void CanCreateNotebook()
        {
            Notebook notebook = new Notebook();
            Assert.DoesNotThrow(() => { webApi.CreateNotebook(notebook); }, "The client API must handle the request successfully!");
        }

        [Test]
        public void CanDeleteNotebook()
        {
            int id = 0;

            ErrorResponse response = null;

            Assert.DoesNotThrow(() => { response = webApi.DeleteNotebook(id); }, "The client API must handle the request successfully!");
            Assert.IsNotNull(response, "The API must return a response!");
            Assert.False(response.HasError, "The API call must be successful!");
        }

        [Test]
        public void CanUpdateNotebook()
        {
            Notebook notebook = new Notebook()
            {
                Name = "TestNotebook",
                Stacks = new List<Stack>()
                {
                    new Stack() { Name = "TestStack1" },
                    new Stack() { Name = "TestStack2" },
                    new Stack() { Name = "TestStack3" }
                }
            };

            ErrorResponse response = null;

            Assert.DoesNotThrow(() => { response = webApi.UpdateNotebook(notebook); }, "The client API must handle the request successfully!");
            Assert.IsNotNull(response, "The API must return a response!");
            Assert.False(response.HasError, "The API call must be successful!");
        }
    }
}
