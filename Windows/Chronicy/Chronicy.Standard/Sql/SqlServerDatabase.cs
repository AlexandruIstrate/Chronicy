using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chronicy.Sql
{
    // This is a temporary class. In the future it will be replaced by something more
    // flexible using reflection and dynamic code generation
    public class SqlServerDatabase : ISqlDatabase
    {
        private object databaseLock = new object();

        public SqlConnection Connection { get; set; }

        public SqlServerDatabase(SqlConnection connection)
        {
            Connection = connection;
        }

        public void Dispose()
        {
            Connection.Dispose();
            GC.SuppressFinalize(this);
        }

        public DataSet RunScalarString(string query, List<SqlParameter> parameters = null)
        {
            lock (databaseLock)
            {

                try
                {
                    Connection.Open();

                    using (SqlCommand command = new SqlCommand(query, Connection))
                    {
                        command.CommandType = CommandType.Text;

                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters.ToArray());
                        }

                        DataSet dataSet = new DataSet();

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
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
        }

        public async Task<DataSet> RunScalarStringAsync(string query, List<SqlParameter> parameters = null)
        {
            return Task.FromResult(RunScalarString(query, parameters));
        }

        public int RunNonQueryString(string query, List<SqlParameter> parameters = null)
        {
            lock (databaseLock)
            {

                try
                {
                    Connection.Open();

                    using (SqlCommand command = new SqlCommand(query, Connection))
                    {
                        command.CommandType = CommandType.Text;

                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters.ToArray());
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
        }

        public async Task<int> RunNonQueryStringAsync(string query, List<SqlParameter> parameters = null)
        {
            return Task.FromResult(RunNonQueryString(query, parameters));
        }

        public DataSet RunScalarProcedure(string procedureName, List<SqlParameter> parameters = null)
        {
            lock (databaseLock)
            {

                try
                {
                    Connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = Connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = procedureName;

                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters.ToArray());
                        }

                        DataSet dataSet = new DataSet();

                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                        {
                            dataAdapter.Fill(dataSet);
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
        }

        public async Task<DataSet> RunScalarProcedureAsync(string procedureName, List<SqlParameter> parameters = null)
        {
            return Task.FromResult(RunScalarProcedure(procedureName, parameters));
        }

        public int RunNonQueryProcedure(string procedureName, List<SqlParameter> parameters = null)
        {
            lock (databaseLock)
            {

                try
                {
                    Connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = Connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = procedureName;

                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters.ToArray());
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
        }

        public async Task<int> RunNonQueryProcedureAsync(string procedureName, List<SqlParameter> parameters = null)
        {
            return Task.FromResult(RunNonQueryProcedure(procedureName, parameters));
        }
    }
}
