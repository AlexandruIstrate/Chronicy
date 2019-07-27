using Chronicy.Sql;
using Chronicy.Web.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

// TODO: Change this to use the global Chronicy implementation of ChronicyUser
using ChronicyUser = Microsoft.AspNetCore.Identity.IdentityUser<int>;

namespace Chronicy.Web.Api
{
    public class AuthenticationApi : IAuthentication
    {
        private readonly SqlServerDatabase database;

        public AuthenticationApi(SqlServerDatabase database)
        {
            this.database = database;
        }

        public Token Authenticate(string username, string password)
        {
            try
            {
                string hash = GetUserPasswordHash(username);
                bool isOk = CheckPassword(password, hash);

                if (!isOk)
                {
                    throw new Exception("Invalid username or password");
                }

                string token = GetUserToken(username);

                return new Token { AccessToken = token };
            }
            catch (Exception e)
            {
                // TODO: Change this to a JSON error respose
                throw new Exception("Could not authenticate", e);
            }
        }

        public async Task<Token> AuthenticateAsync(string username, string password)
        {
            try
            {
                string hash = await GetUserPasswordHashAsync(username);
                bool isOk = CheckPassword(password, hash);

                if (!isOk)
                {
                    throw new Exception("Invalid username or password");
                }

                string token = await GetUserTokenAsync(username);

                return new Token { AccessToken = token };
            }
            catch (Exception e)
            {
                // TODO: Change this to a JSON error respose
                throw new Exception("Could not authenticate", e);
            }
        }

        public string GetUserPasswordHash(string username)
        {
            try
            {
                DataSet dataSet = database.RunScalarProcedure(SqlProcedures.User.Read, new List<SqlParameter>
                {
                    new SqlParameter(Parameters.UserName, username)
                });

                DataTable dataTable = dataSet.Tables[0];
                DataRow dataRow = dataTable.Rows[0];

                return (string)dataRow[Columns.PasswordHash];
            }
            catch (IndexOutOfRangeException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> GetUserPasswordHashAsync(string username)
        {
            try
            {
                DataSet dataSet = await database.RunScalarProcedureAsync(SqlProcedures.User.Read, new List<SqlParameter>
                {
                    new SqlParameter(Parameters.UserName, username)
                });

                DataTable dataTable = dataSet.Tables[0];
                DataRow dataRow = dataTable.Rows[0];

                return (string)dataRow[Columns.PasswordHash];
            }
            catch (IndexOutOfRangeException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetUserToken(string username)
        {
            try
            {
                DataSet dataSet = database.RunScalarProcedure(SqlProcedures.User.AuthenticateForToken, new List<SqlParameter>
                {
                    new SqlParameter(Parameters.UserName, username)
                });

                DataTable dataTable = dataSet.Tables[0];
                DataRow dataRow = dataTable.Rows[0];

                return (string)dataRow[Columns.Token];
            }
            catch (IndexOutOfRangeException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> GetUserTokenAsync(string username)
        {
            try
            {
                DataSet dataSet = await database.RunScalarProcedureAsync(SqlProcedures.User.AuthenticateForToken, new List<SqlParameter>
                {
                    new SqlParameter(Parameters.UserName, username)
                });

                DataTable dataTable = dataSet.Tables[0];
                DataRow dataRow = dataTable.Rows[0];

                return (string)dataRow[Columns.Token];
            }
            catch (IndexOutOfRangeException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool CheckPassword(string password, string hashedPassword)
        {
            IPasswordHasher<ChronicyUser> passwordHasher = new PasswordHasher<ChronicyUser>();
            PasswordVerificationResult result = passwordHasher.VerifyHashedPassword(new ChronicyUser(), hashedPassword, password);
            return result != PasswordVerificationResult.Failed;
        }

        private class Parameters
        {
            public const string UserName = "@username";
        }

        private class Columns
        {
            public const string PasswordHash = "passwordHash";
            public const string Token = "token";
        }
    }
}
