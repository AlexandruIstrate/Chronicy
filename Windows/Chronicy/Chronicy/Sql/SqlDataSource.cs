using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Chronicy.Data;
using Chronicy.Data.Storage;

namespace Chronicy.Sql
{
    /// <summary>
    /// Provides data source operations on an SQL database.
    /// </summary>
    public class SqlDataSource : IDataSource<Notebook>, IDisposable
    {
        private readonly ISqlDatabase database;

        public SqlDataSource(SqlConnection connection)
        {
            database = new SqlServerDatabase(connection);
        }

        public SqlDataSource(ISqlDatabase database)
        {
            this.database = database;
        }

        public void Dispose()
        {
            database.Dispose();
            GC.SuppressFinalize(this);
        }

        public void Create(Notebook item)
        {
            database.RunNonQueryProcedure(SqlProcedures.Notebook.Create, new List<SqlParameter>
            {
                new SqlParameter(nameof(item.Name), item.Name),
                new SqlParameter(nameof(item.Stacks), item.Stacks)
            });
        }

        public Task CreateAsync(Notebook item)
        {
            return database.RunNonQueryProcedureAsync(SqlProcedures.Notebook.Create, new List<SqlParameter>
            {
                new SqlParameter(nameof(item.Name), item.Name),
                new SqlParameter(nameof(item.Stacks), item.Stacks)
            });
        }

        public void Delete(int id)
        {
            database.RunNonQueryProcedure(SqlProcedures.Notebook.Delete, new List<SqlParameter>
            {
                new SqlParameter(nameof(id), id)
            });
        }

        public Task DeleteAsync(int id)
        {
            return database.RunNonQueryProcedureAsync(SqlProcedures.Notebook.Delete, new List<SqlParameter>
            {
                new SqlParameter(nameof(id), id)
            });
        }

        public Notebook Get(int id)
        {
            try
            {
                DataSet dataSet = database.RunScalarProcedure(SqlProcedures.Notebook.Create, new List<SqlParameter>
                {
                    new SqlParameter(nameof(id), id)
                });
                DataTable dataTable = dataSet.Tables[0];

                DataRow row = dataTable.Rows[0];

                // TODO: Fill with data from the row
                Notebook notebook = new Notebook(string.Empty);

                return notebook;
            }
            catch (IndexOutOfRangeException)
            {
                throw new DataSourceException("The DataSource does not contain any DataTables");
            }
        }

        public async Task<Notebook> GetAsync(int id)
        {
            try
            {
                DataSet dataSet = await database.RunScalarProcedureAsync(SqlProcedures.Notebook.Create, new List<SqlParameter>
                {
                    new SqlParameter(nameof(id), id)
                });
                DataTable dataTable = dataSet.Tables[0];

                DataRow row = dataTable.Rows[0];

                // TODO: Fill with data from the row
                Notebook notebook = new Notebook(string.Empty);

                return notebook;
            }
            catch (IndexOutOfRangeException)
            {
                throw new DataSourceException("The DataSource does not contain any DataTables");
            }
        }

        public IEnumerable<Notebook> GetAll()
        {
            try
            {
                DataSet dataSet = database.RunScalarProcedure(SqlProcedures.Notebook.Read);
                DataTable dataTable = dataSet.Tables[0];

                List<Notebook> result = new List<Notebook>();

                foreach (DataRow row in dataTable.Rows)
                {
                    // Get the data from row and put it into the new notebook object
                }

                return result;
            }
            catch (IndexOutOfRangeException)
            {
                throw new DataSourceException("The DataSource does not contain any DataTables");
            }
        }

        public async Task<IEnumerable<Notebook>> GetAllAsync()
        {
            try
            {
                DataSet dataSet = await database.RunScalarProcedureAsync(SqlProcedures.Notebook.Read);
                DataTable dataTable = dataSet.Tables[0];

                List<Notebook> result = new List<Notebook>();

                foreach (DataRow row in dataTable.Rows)
                {
                    // Get the data from row and put it into the new notebook object
                }

                return result;
            }
            catch (IndexOutOfRangeException)
            {
                throw new DataSourceException("The DataSource does not contain any DataTables");
            }
        }

        public void Update(Notebook notebook)
        {
            database.RunNonQueryProcedure(SqlProcedures.Notebook.Update, new List<SqlParameter>
            {
                new SqlParameter(nameof(notebook), notebook)
            });
        }

        public Task UpdateAsync(Notebook notebook)
        {
            return database.RunNonQueryProcedureAsync(SqlProcedures.Notebook.Update, new List<SqlParameter>
            {
                new SqlParameter(nameof(notebook), notebook)
            });
        }
    }
}
