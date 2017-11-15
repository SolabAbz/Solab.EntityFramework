using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solab.EntityFramework.Tests.Helpers
{
    public static class DatabaseHelpers
    {
        public static bool DatabaseExists(this SqlConnection connection, string database)
        {
            foreach (var metadata in connection.Query("SELECT * FROM sys.databases"))
            {
                if (metadata.name.Equals(database, StringComparison.CurrentCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        public static int CompatibilityLevel(this SqlConnection connection)
        {
            return connection.QueryFirst<int>("select compatibility_level from sys.databases where name = DB_NAME()");
        }

        public static void CreateDatabase(this SqlConnection connection, string database)
        {
            connection.Execute($"CREATE DATABASE [{database}]");
        }

        public static void DropDatabase(this SqlConnection connection, string database)
        {
            connection.Execute($"DROP DATABASE [{database}]");
        }

        public static void ExecuteBatch(this SqlConnection connection, string sql)
        {
            foreach(var statement in sql.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                connection.Execute(statement);
            }
        }
    }
}
