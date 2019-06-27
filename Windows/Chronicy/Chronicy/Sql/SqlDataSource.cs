using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chronicy.Data;
using Chronicy.Data.Storage;

namespace Chronicy.Sql
{
    public class SqlDataSource : IDataSource
    {
        private ProcedureRunner runner;
        private DataSetConverter converter;

        public SqlDataSource()
        {
            // TODO: SqlConnection
            runner = new ProcedureRunner(null);
            converter = new DataSetConverter();
        }

        public Notebook CreateNotebook(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Notebook> CreateNotebookAsync(string name)
        {
            throw new NotImplementedException();
        }

        public void DeleteNotebook(Notebook notebook)
        {
            throw new NotImplementedException();
        }

        public Task DeleteNotebookAsync(Notebook notebook)
        {
            throw new NotImplementedException();
        }

        public Notebook GetNotebook(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Notebook> GetNotebookAsync(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Notebook> GetNotebooks()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Notebook>> GetNotebooksAsync()
        {
            throw new NotImplementedException();
        }

        public void UpdateNotebook(Notebook notebook)
        {
            throw new NotImplementedException();
        }

        public Task UpdateNotebookAsync(Notebook notebook)
        {
            throw new NotImplementedException();
        }
    }
}
