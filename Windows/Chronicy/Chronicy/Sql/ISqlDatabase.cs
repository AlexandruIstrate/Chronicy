using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Chronicy.Sql
{
    /// <summary>
    /// An abstract representation of a database
    /// </summary>
    public interface ISqlDatabase : IDisposable
    {
        /// <summary>
        /// Runs the <paramref name="query"/> string using the given <paramref name="parameters"/>.
        /// This method is intended for queries that return data.
        /// </summary>
        /// <param name="query">The SQL query string</param>
        /// <param name="parameters">A list of parameters</param>
        /// <returns>A dataset containing the data from the database</returns>
        DataSet RunScalarString(string query, List<SqlParameter> parameters = null);

        /// <summary>
        /// Runs the <paramref name="query"/> string using the given <paramref name="parameters"/> asynchronously
        /// This method is intended for queries that return data.
        /// </summary>
        /// <param name="query">The SQL query string</param>
        /// <param name="parameters">A list of parameters</param>
        /// <returns>A task that represents the dataset containing the data from the database</returns>
        Task<DataSet> RunScalarStringAsync(string query, List<SqlParameter> parameters = null);

        /// <summary>
        /// Runs the <paramref name="query"/> string using the given <paramref name="parameters"/>.
        /// This method is intended for queries that don't return data.
        /// </summary>
        /// <param name="query">The SQL query string</param>
        /// <param name="parameters">A list of parameters</param>
        /// <returns>The number of rows affected</returns>
        int RunNonQueryString(string query, List<SqlParameter> parameters = null);

        /// <summary>
        /// Runs the <paramref name="query"/> string using the given <paramref name="parameters"/>.
        /// This method is intended for queries that don't return data.
        /// </summary>
        /// <param name="query">The SQL query string</param>
        /// <param name="parameters">A list of parameters</param>
        /// <returns>A task that represents the number of rows affected</returns>
        Task<int> RunNonQueryStringAsync(string query, List<SqlParameter> parameters = null);

        /// <summary>
        /// Runs the stored procedure with the given <paramref name="parameters"/>
        /// This method is intended for procedures that return data.
        /// </summary>
        /// <param name="procedureName">The name of the procedure</param>
        /// <param name="parameters">A list of parameters</param>
        /// <returns>A dataset containing the data from the database</returns>
        DataSet RunScalarProcedure(string procedureName, List<SqlParameter> parameters = null);

        /// <summary>
        /// Runs the stored procedure with the given <paramref name="parameters"/>
        /// This method is intended for procedures that return data.
        /// </summary>
        /// <param name="procedureName">The name of the procedure</param>
        /// <param name="parameters">A list of parameters</param>
        /// <returns>A task that represents a dataset containing the data from the database</returns>
        Task<DataSet> RunScalarProcedureAsync(string procedureName, List<SqlParameter> parameters = null);

        /// <summary>
        /// Runs the stored procedure with the given <paramref name="parameters"/>
        /// This method is intended for procedures that don't return data.
        /// </summary>
        /// <param name="procedureName">The name of the procedure</param>
        /// <param name="parameters">A list of parameters</param>
        /// <returns>A dataset containing the data from the database</returns>
        int RunNonQueryProcedure(string procedureName, List<SqlParameter> parameters = null);

        /// <summary>
        /// Runs the stored procedure with the given <paramref name="parameters"/>
        /// This method is intended for procedures that don't return data.
        /// </summary>
        /// <param name="procedureName">The name of the procedure</param>
        /// <param name="parameters">A list of parameters</param>
        /// <returns>A task that represents a dataset containing the data from the database</returns>
        Task<int> RunNonQueryProcedureAsync(string procedureName, List<SqlParameter> parameters = null);
    }
}
