using Chronicy.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Common;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Data.SQLite.EF6;
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

        /// <summary>
        /// Represents the context used for mapping entities
        /// </summary>
        public SQLiteDatabaseContext Context { get; set; }

        /// <summary>
        /// Creates a database with a <see cref="SQLiteDatabaseContext"/> using the specified <see cref="SQLiteConnection"/>
        /// </summary>
        /// <param name="connection">The connection to use</param>
        public SQLiteDatabase(SQLiteConnection connection)
        {
            Context = new SQLiteDatabaseContext(connection);
            Connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        /// <summary>
        /// Creates a database by automatically creating a <see cref="SQLiteConnection"/> and a <see cref="SQLiteDatabaseContext"/>
        /// </summary>
        public SQLiteDatabase()
        {
            Context = new SQLiteDatabaseContext();
            Connection = (SQLiteConnection)Context.Database.Connection;
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
    }

    public class SQLiteConfiguration : DbConfiguration
    {
        private const string SQLiteNamespace = "System.Data.SQLite";
        private const string EntityFrameworkNamespace = "System.Data.SQLite.EF6";

        public SQLiteConfiguration()
        {
            SetProviderFactory(SQLiteNamespace, SQLiteFactory.Instance);
            SetProviderFactory(EntityFrameworkNamespace, SQLiteProviderFactory.Instance);
            SetProviderServices(SQLiteNamespace, (DbProviderServices) SQLiteProviderFactory.Instance.GetService(typeof(DbProviderServices)));
        }
    }

    public class SQLiteDatabaseContext : DbContext
    {
        public DbSet<Notebook> Notebooks { get; set; }

        public SQLiteDatabaseContext(SQLiteConnection connection = null) 
            : base(connection ?? CreateConnection(), contextOwnsConnection: true)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        internal static SQLiteConnection CreateConnection()
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

    public class SQLiteInitializer : DropCreateDatabaseIfModelChanges<SQLiteDatabaseContext>
    {
        protected override void Seed(SQLiteDatabaseContext context)
        {
            context.Notebooks.Add(new Notebook("Test"));
        }
    }
}
