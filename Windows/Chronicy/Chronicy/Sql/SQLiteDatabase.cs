using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Threading.Tasks;

namespace Chronicy.Sql
{
    public class SQLiteDatabase : ISqlDatabase
    {
        /// <summary>
        /// Represents the connection that is used for queries
        /// </summary>
        public SQLiteConnection Connection { get; set; }

        public SQLiteDatabase(SQLiteConnection connection = null)
        {
            Connection = connection ?? CreateConnection();
        }

        public void Dispose()
        {
            Connection.Dispose();
            GC.SuppressFinalize(this);
        }

        public DataSet RunScalarString(string query, List<SqlParameter> parameters = null)
        {
            try
            {
                Connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(query, Connection))
                {
                    command.CommandType = CommandType.Text;

                    if (parameters != null)
                    {
                        parameters.ForEach((param) => command.Parameters.AddWithValue(param.ParameterName, param.Value));
                    }

                    DataSet dataSet = new DataSet();

                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                    }

                    return dataSet;
                }                
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Connection.Close();
            }
        }

        public Task<DataSet> RunScalarStringAsync(string query, List<SqlParameter> parameters = null)
        {
            return Task.Run(() =>
            {
                return RunScalarString(query, parameters);
            });
        }

        public int RunNonQueryString(string query, List<SqlParameter> parameters = null)
        {
            try
            {
                Connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(query, Connection))
                {
                    command.CommandType = CommandType.Text;

                    if (parameters != null)
                    {
                        parameters.ForEach((param) => command.Parameters.AddWithValue(param.ParameterName, param.Value));
                    }

                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Connection.Close();
            }
        }

        public Task<int> RunNonQueryStringAsync(string query, List<SqlParameter> parameters = null)
        {
            return Task.Run(() =>
            {
                return RunNonQueryString(query, parameters);
            });
        }

        public DataSet RunScalarProcedure(string procedureName, List<SqlParameter> parameters = null)
        {
            throw new NotSupportedException(NotSupportedMessage);
        }

        public Task<DataSet> RunScalarProcedureAsync(string procedureName, List<SqlParameter> parameters = null)
        {
            throw new NotSupportedException(NotSupportedMessage);
        }

        public int RunNonQueryProcedure(string procedureName, List<SqlParameter> parameters = null)
        {
            throw new NotSupportedException(NotSupportedMessage);
        }

        public Task<int> RunNonQueryProcedureAsync(string procedureName, List<SqlParameter> parameters = null)
        {
            throw new NotSupportedException(NotSupportedMessage);
        }

        private const string NotSupportedMessage = "SQLite does not support stored procedures";

        private SQLiteConnection CreateConnection()
        {
            string savePath = AppDomain.CurrentDomain.BaseDirectory;
            string fileName = "Chronicy.sqlite";

            string dataSource = Path.Combine(savePath, fileName);

            SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder()
            {
                DataSource = dataSource,
                ForeignKeys = true
            };

            return new SQLiteConnection(builder.ConnectionString);
        }
    }
}
