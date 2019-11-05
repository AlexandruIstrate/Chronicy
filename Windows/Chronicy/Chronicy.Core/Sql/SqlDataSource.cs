using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Chronicy.Data;
using Chronicy.Data.Storage;
using Chronicy.Standard.Data;
using Newtonsoft.Json;

namespace Chronicy.Sql
{
    // TODO: Figure out whether it's better to use Data notebooks or Web notebooks
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
            try
            {
                database.RunNonQueryProcedure(SqlProcedures.Notebook.CreateGraph, new List<SqlParameter>
                {
                    new SqlParameter(Parameters.Json, JsonConvert.SerializeObject(item))
                });
            }
            catch (JsonException e)
            {
                throw new DataSourceException("Could not serialize Notebook", e);
            }
            catch (Exception e)
            {
                throw new DataSourceException("Could not create Notebook", e);
            }
        }

        public async Task CreateAsync(Notebook item)
        {
            try
            {
                await database.RunNonQueryProcedureAsync(SqlProcedures.Notebook.CreateGraph, new List<SqlParameter>
                {
                    new SqlParameter(Parameters.Json, JsonConvert.SerializeObject(item))
                });
            }
            catch (JsonException e)
            {
                throw new DataSourceException("Could not serialize Notebook", e);
            }
            catch (Exception e)
            {
                throw new DataSourceException("Could not create Notebook", e);
            }
        }

        public void Delete(int id)
        {
            try
            {
                database.RunNonQueryProcedure(SqlProcedures.Notebook.DeleteGraph, new List<SqlParameter>
                {
                    new SqlParameter(Parameters.ID, id)
                });
            }
            catch (Exception e)
            {
                throw new DataSourceException("Could not delete Notebook", e);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await database.RunNonQueryProcedureAsync(SqlProcedures.Notebook.DeleteGraph, new List<SqlParameter>
                {
                    new SqlParameter(Parameters.ID, id)
                });
            }
            catch (Exception e)
            {
                throw new DataSourceException("Could not delete Notebook", e);
            }
        }

        public Notebook Get(int id)
        {
            try
            {
                DataSet dataSet = database.RunScalarProcedure(SqlProcedures.Notebook.ReadGraph, new List<SqlParameter>
                {
                    new SqlParameter(Parameters.ID, id)
                });

                DataTable dataTable = dataSet.Tables[0];
                DataRow row = dataTable.Rows[0];

                string json = (string)row[Columns.Json];

                // The database returns a JSON array with one element
                IEnumerable<Notebook> notebookArray = JsonConvert.DeserializeObject<IEnumerable<Notebook>>(json);
                return notebookArray.First();
            }
            catch (IndexOutOfRangeException e)
            {
                throw new DataSourceException($"The Notebook with ID { id } does not exist", e);
            }
            catch (InvalidOperationException e)
            {
                throw new DataSourceException($"The Notebook with ID { id } does not exist", e);
            }
            catch (JsonException e)
            {
                throw new DataSourceException($"Could not deserialize Notebook JSON", e);
            }
            catch (Exception e)
            {
                throw new DataSourceException("Could not get Notebook", e);
            }
        }

        public async Task<Notebook> GetAsync(int id)
        {
            try
            {
                DataSet dataSet = await database.RunScalarProcedureAsync(SqlProcedures.Notebook.ReadGraph, new List<SqlParameter>
                {
                    new SqlParameter(Parameters.ID, id)
                });

                DataTable dataTable = dataSet.Tables[0];
                DataRow row = dataTable.Rows[0];

                string json = (string)row[Columns.Json];

                // The database returns a JSON array with one element
                IEnumerable<Notebook> notebookArray = JsonConvert.DeserializeObject<IEnumerable<Notebook>>(json);
                return notebookArray.First();
            }
            catch (IndexOutOfRangeException e)
            {
                throw new DataSourceException($"The Notebook with ID { id } does not exist", e);
            }
            catch (InvalidOperationException e)
            {
                throw new DataSourceException($"The Notebook with ID { id } does not exist", e);
            }
            catch (JsonException e)
            {
                throw new DataSourceException($"Could not deserialize Notebook JSON", e);
            }
            catch (Exception e)
            {
                throw new DataSourceException("Could not get Notebook", e);
            }
        }

        public IEnumerable<Notebook> GetAll()
        {
            try
            {
                DataSet dataSet = database.RunScalarProcedure(SqlProcedures.Notebook.ReadGraph);
                DataTable dataTable = dataSet.Tables[0];

                List<Notebook> notebooks = new List<Notebook>();

                foreach (DataRow row in dataTable.Rows)
                {
                    string json = (string)row[Columns.Json];

                    // The database returns a JSON array with one element
                    IEnumerable<Notebook> notebookArray = JsonConvert.DeserializeObject<IEnumerable<Notebook>>(json);
                    notebooks.Add(notebookArray.First());
                }

                return notebooks;
            }
            catch (JsonException e)
            {
                throw new DataSourceException($"Could not deserialize Notebook JSON", e);
            }
            catch (Exception e)
            {
                throw new DataSourceException("Could not get Notebook", e);
            }
        }

        public async Task<IEnumerable<Notebook>> GetAllAsync()
        {
            try
            {
                DataSet dataSet = await database.RunScalarProcedureAsync(SqlProcedures.Notebook.ReadGraph);
                DataTable dataTable = dataSet.Tables[0];

                List<Notebook> notebooks = new List<Notebook>();

                foreach (DataRow row in dataTable.Rows)
                {
                    string json = (string)row[Columns.Json];

                    // The database returns a JSON array with one element
                    IEnumerable<Notebook> notebookArray = JsonConvert.DeserializeObject<IEnumerable<Notebook>>(json);
                    notebooks.Add(notebookArray.First());
                }

                return notebooks;
            }
            catch (JsonException e)
            {
                throw new DataSourceException($"Could not deserialize Notebook JSON", e);
            }
            catch (Exception e)
            {
                throw new DataSourceException("Could not get Notebook", e);
            }
        }

        public void Update(Notebook notebook)
        {
            try
            {
                database.RunNonQueryProcedure(SqlProcedures.Notebook.UpdateGraph, new List<SqlParameter>
                {
                    new SqlParameter(Parameters.ID, notebook.ID),
                    new SqlParameter(Parameters.Json, JsonConvert.SerializeObject(notebook))
                });
            }
            catch (JsonException e)
            {
                throw new DataSourceException($"Could not deserialize Notebook JSON", e);
            }
            catch (Exception e)
            {
                throw new DataSourceException("Could not update Notebook", e);
            }
        }

        public async Task UpdateAsync(Notebook notebook)
        {
            try
            {
                await database.RunNonQueryProcedureAsync(SqlProcedures.Notebook.UpdateGraph, new List<SqlParameter>
                {
                    new SqlParameter(Parameters.ID, notebook.ID),
                    new SqlParameter(Parameters.Json, JsonConvert.SerializeObject(notebook))
                });
            }
            catch (JsonException e)
            {
                throw new DataSourceException($"Could not deserialize Notebook JSON", e);
            }
            catch (Exception e)
            {
                throw new DataSourceException("Could not update Notebook", e);
            }
        }

        public static class Parameters
        {
            public static string ID = "@idnotebook";
            public static string Json = "@json";
        }

        public static class Columns
        {
            public static string Json = "nbjson";
        }
    }
}
