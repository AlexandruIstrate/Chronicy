using Chronicy.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;

namespace Chronicy.Sql
{
    public class SqliteDatabase : ISqlDatabase
    {
        /// <summary>
        /// Represents the connection that is used for queries
        /// </summary>
        public SqliteConnection Connection { get; set; }

        /// <summary>
        /// Represents the context used for mapping entities
        /// </summary>
        public SQLiteDatabaseContext Context { get; set; }

        /// <summary>
        /// Creates a database with a <see cref="SQLiteDatabaseContext"/> using the specified <see cref="SQLiteConnection"/>
        /// </summary>
        /// <param name="connection">The connection to use</param>
        public SqliteDatabase(SqliteConnection connection)
        {
            Connection = connection ?? throw new ArgumentNullException(nameof(connection));
            Context = new SQLiteDatabaseContext(connection);
        }

        /// <summary>
        /// Creates a database by automatically creating a <see cref="SQLiteConnection"/> and a <see cref="SQLiteDatabaseContext"/>
        /// </summary>
        public SqliteDatabase()
        {
            Context = new SQLiteDatabaseContext();
            //Connection = (SQLiteConnection)Context.Database.GetDbConnection();
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

                using (SqliteCommand command = new SqliteCommand(query, Connection))
                {
                    command.CommandType = CommandType.Text;

                    if (parameters != null)
                    {
                        parameters.ForEach((param) => command.Parameters.AddWithValue(param.ParameterName, param.Value));
                    }

                    DataSet dataSet = new DataSet();

                    //using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    //{
                    //    adapter.Fill(dataSet);
                    //}

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

                using (SqliteCommand command = new SqliteCommand(query, Connection))
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
    }

    public class SQLiteDatabaseContext : DbContext
    {
        private readonly SqliteConnection connection;

        public DbSet<Notebook> Notebooks { get; set; }
        public DbSet<Stack> Stacks { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<CustomField> Fields { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public SQLiteDatabaseContext(SqliteConnection connection = null)
        {
            this.connection = connection ?? CreateConnection();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connection);
            //SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
        }

        internal static SqliteConnection CreateConnection()
        {
            string savePath = AppDomain.CurrentDomain.BaseDirectory;
            string fileName = "Chronicy.sqlite";

            string dataSource = Path.Combine(savePath, fileName);

            SqliteConnectionStringBuilder builder = new SqliteConnectionStringBuilder()
            {
                DataSource = dataSource
            };

            return new SqliteConnection(builder.ConnectionString);
        }
    }
}
