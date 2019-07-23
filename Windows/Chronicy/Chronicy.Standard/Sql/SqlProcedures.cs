namespace Chronicy.Sql
{
    public static class SqlProcedures
    {
        public static class User
        {
            public const string Create = "dbo.UserCreate";
            public const string Read = "dbo.UserRead";
            public const string Update = "dbo.UserUpdate";
            public const string Delete = "dbo.UserDelete";

            public const string AuthenticateForToken = "dbo.UserAuthCheck";
            public const string CheckToken = "dbo.UserTokenCheck";
        }

        public static class Role
        {
            public const string Create = "dbo.RoleCreate";
            public const string Read = "dbo.RoleRead";
            public const string Update = "dbo.RoleUpdate";
            public const string Delete = "dbo.RoleDelete";
        }

        public static class Notebook
        {
            public const string Create = "CreateNotebook";
            public const string Read = "GetNotebook";
            public const string Update = "UpdateNotebook";
            public const string Delete = "DeleteNotebook";

            public const string CreateGraph = "dbo.NGraphCreate";
            public const string ReadGraph = "dbo.NGraphRead";
        }
    }
}
