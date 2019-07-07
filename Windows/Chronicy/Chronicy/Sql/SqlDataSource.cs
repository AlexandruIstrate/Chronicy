using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Chronicy.Data;
using Chronicy.Data.Storage;

namespace Chronicy.Sql
{
    public class SqlDataSource : IDataSource<Notebook>, IDisposable
    {
        private ISqlDatabase database;

        public SqlDataSource(SqlConnection connection)
        {
            database = new SqlServerDatabase(connection);
        }

        public void Dispose()
        {
            database.Dispose();
            GC.SuppressFinalize(this);
        }

        public void Create(Notebook item)
        {
            database.RunNonQueryProcedure(SqlProcedures.CreateNotebook, new List<SqlParameter>
            {
                new SqlParameter(nameof(item.Name), item.Name),
                new SqlParameter(nameof(item.Stacks), item.Stacks)
            });
        }

        public Task CreateAsync(Notebook item)
        {
            return database.RunNonQueryProcedureAsync(SqlProcedures.CreateNotebook, new List<SqlParameter>
            {
                new SqlParameter(nameof(item.Name), item.Name),
                new SqlParameter(nameof(item.Stacks), item.Stacks)
            });
        }

        public void Delete(string id)
        {
            database.RunNonQueryProcedure(SqlProcedures.DeleteNotebook, new List<SqlParameter>
            {
                new SqlParameter(nameof(id), id)
            });
        }

        public Task DeleteAsync(string id)
        {
            return database.RunNonQueryProcedureAsync(SqlProcedures.DeleteNotebook, new List<SqlParameter>
            {
                new SqlParameter(nameof(id), id)
            });
        }

        public Notebook Get(string id)
        {
            try
            {
                DataSet dataSet = database.RunScalarProcedure(SqlProcedures.CreateNotebook, new List<SqlParameter>
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

        public async Task<Notebook> GetAsync(string id)
        {
            try
            {
                DataSet dataSet = await database.RunScalarProcedureAsync(SqlProcedures.CreateNotebook, new List<SqlParameter>
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
                DataSet dataSet = database.RunScalarProcedure(SqlProcedures.GetNotebooks);
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
                DataSet dataSet = await database.RunScalarProcedureAsync(SqlProcedures.GetNotebooks);
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
            database.RunNonQueryProcedure(SqlProcedures.UpdateNotebook, new List<SqlParameter>
            {
                new SqlParameter(nameof(notebook), notebook)
            });
        }

        public Task UpdateAsync(Notebook notebook)
        {
            return database.RunNonQueryProcedureAsync(SqlProcedures.UpdateNotebook, new List<SqlParameter>
            {
                new SqlParameter(nameof(notebook), notebook)
            });
        }
    }
}
