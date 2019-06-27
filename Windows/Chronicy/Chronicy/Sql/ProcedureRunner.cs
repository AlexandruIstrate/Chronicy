using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chronicy.Sql
{
    // This is a temporary class. In the future it will be replaced by something more
    // flexible using reflection and dynamic code generation
    public class ProcedureRunner : IDisposable
    {
        public SqlConnection Connection { get; set; }

        public ProcedureRunner(SqlConnection connection)
        {
            Connection = connection;
        }

        public void Dispose()
        {
            Connection.Dispose();
            GC.SuppressFinalize(this);
        }

        public DataSet RunScalar(string name, List<SqlParameter> parameters)
        {
            try
            {
                Connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = Connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = name;
                    command.Parameters.AddRange(parameters.ToArray());

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

        public Task<DataSet> RunScalarAsync(string name, List<SqlParameter> parameters)
        {
            return Task.Run(() =>
            {
                return RunScalar(name, parameters);
            });
        }

        public int RunNonQuery(string name, List<SqlParameter> parameters)
        {
            try
            {
                Connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = Connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = name;
                    command.Parameters.AddRange(parameters.ToArray());
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

        public Task<int> RunNonQueryAsync(string name, List<SqlParameter> parameters)
        {
            return Task.Run(() =>
            {
                return RunNonQuery(name, parameters);
            });
        }
    }
}
