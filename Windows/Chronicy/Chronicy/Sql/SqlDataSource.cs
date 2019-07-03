﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Chronicy.Data;
using Chronicy.Data.Storage;

namespace Chronicy.Sql
{
    public class SqlDataSource : IDataSource<Notebook>
    {
        private ProcedureRunner runner;

        public SqlDataSource(SqlConnection connection)
        {
            runner = new ProcedureRunner(connection);
        }

        public void Create(Notebook item)
        {
            try
            {
                runner.RunNonQuery(Procedures.CreateNotebook, new List<SqlParameter>
                {
                    new SqlParameter(nameof(item.Name), item.Name),
                    new SqlParameter(nameof(item.Stacks), item.Stacks)
                });
            }
            catch (IndexOutOfRangeException)
            {
                throw new DataSourceException("The DataSource does not contain any DataTables");
            }
        }

        public Task CreateAsync(Notebook item)
        {
            try
            {
                return runner.RunNonQueryAsync(Procedures.CreateNotebook, new List<SqlParameter>
                {
                    new SqlParameter(nameof(item.Name), item.Name),
                    new SqlParameter(nameof(item.Stacks), item.Stacks)
                });
            }
            catch (IndexOutOfRangeException)
            {
                throw new DataSourceException("The DataSource does not contain any DataTables");
            }
        }

        public void Delete(string id)
        {
            runner.RunNonQuery(Procedures.DeleteNotebook, new List<SqlParameter>
            {
                new SqlParameter(nameof(id), id)
            });
        }

        public Task DeleteAsync(string id)
        {
            return runner.RunNonQueryAsync(Procedures.DeleteNotebook, new List<SqlParameter>
            {
                new SqlParameter(nameof(id), id)
            });
        }

        public Notebook Get(string id)
        {
            try
            {
                DataSet dataSet = runner.RunScalar(Procedures.CreateNotebook, new List<SqlParameter>
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
                DataSet dataSet = await runner.RunScalarAsync(Procedures.CreateNotebook, new List<SqlParameter>
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
                DataSet dataSet = runner.RunScalar(Procedures.GetNotebooks);
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
                DataSet dataSet = await runner.RunScalarAsync(Procedures.GetNotebooks);
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
            runner.RunNonQuery(Procedures.UpdateNotebook, new List<SqlParameter>
            {
                new SqlParameter(nameof(notebook), notebook)
            });
        }

        public Task UpdateAsync(Notebook notebook)
        {
            return runner.RunNonQueryAsync(Procedures.UpdateNotebook, new List<SqlParameter>
            {
                new SqlParameter(nameof(notebook), notebook)
            });
        }
    }
}