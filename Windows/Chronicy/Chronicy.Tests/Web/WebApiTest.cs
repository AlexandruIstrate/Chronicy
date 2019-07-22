using Chronicy.Web;
using Chronicy.Web.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace Chronicy.Tests.Web
{
    [TestFixture]
    public class WebApiTest
    {
        private const string address = "Fill this in";
        private readonly ChronicyWebApi webApi = new ChronicyWebApi(address);

        public string TrackedNotebookName { get; set; }
        public int ID { get; set; }

        public WebApiTest()
        {
            TrackedNotebookName = "TestNotebook";
            ID = default;
        }

        // Authenticate
        [Test]
        public void CanAuthenticate()
        {
            const string username = "Fill this in";
            const string password = "Fill this in";

            Token token = null;

            Assert.DoesNotThrow(() => { token = webApi.Authenticate(username, password); }, "The client API must handle the request successfully");
            Assert.IsNotNull(token, "The token response must not be null");
            Assert.False(token.HasError, "The API call must be successful");

            Assert.IsNotNull(token.AccessToken, "The response must contain a token");
            Assert.IsNotEmpty(token.AccessToken, "The token must be valid");
        }

        // Create
        [Test]
        public void CanCreateNotebook()
        {
            Notebook notebook = new Notebook
            {
                ID = ID,
                Name = TrackedNotebookName
            };

            Assert.DoesNotThrow(() => { webApi.CreateNotebook(notebook); }, "The client API must handle the request successfully");

            // Update ID
            ID = webApi.GetNotebooks().List.Find((item) => item.Name == TrackedNotebookName).ID;
        }

        // Read All
        [Test]
        public void CanGetNotebooks()
        {
            ListResponse<Notebook> notebooks = null;

            Assert.DoesNotThrow(() => { notebooks = webApi.GetNotebooks(); }, "The client API must handle the request successfully");
            Assert.IsNotNull(notebooks, "The notebook list response must not be null");
            Assert.False(notebooks.HasError, "The API call must be successful");

            Assert.IsNotNull(notebooks.List, "The response must contain a list of Notebooks");
        }

        // Read
        [Test]
        public void CanGetNotebook()
        {
            Notebook notebook = null;

            Assert.DoesNotThrow(() => { notebook = webApi.GetNotebook(ID); }, "The client API must handle the request successfully");
            Assert.IsNotNull(notebook, "The notebook response must not be null");
            Assert.False(notebook.HasError, "The API call must be successful");
        }

        // Update
        [Test]
        public void CanModifyNotebook()
        {
            Notebook notebook = new Notebook()
            {
                ID = ID,
                Name = TrackedNotebookName,
                Stacks = new List<Stack>()
                {
                    new Stack() { Name = "TestStack1" },
                    new Stack() { Name = "TestStack2" },
                    new Stack() { Name = "TestStack3" }
                }
            };

            ErrorResponse response = null;

            Assert.DoesNotThrow(() => { response = webApi.UpdateNotebook(notebook); }, "The client API must handle the request successfully");
            Assert.IsNotNull(response, "The API must return a response");
            Assert.False(response.HasError, "The API call must be successful");
        }

        // Delete
        [Test]
        public void CanRemoveNotebook()
        {
            ErrorResponse response = null;

            Assert.DoesNotThrow(() => { response = webApi.DeleteNotebook(ID); }, "The client API must handle the request successfully");
            Assert.IsNotNull(response, "The API must return a response");
            Assert.False(response.HasError, "The API call must be successful");
        }
    }
}
