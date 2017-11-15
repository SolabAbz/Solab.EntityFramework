using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solab.EntityFramework.Tests.Helpers
{
    public static class TableHelpers
    {
        public static void CreateSimpleTable(this SqlConnection connection, string table = "SimpleTable")
        {
            connection.Execute($@"
                CREATE TABLE [{table}]
                (
	                [Id] UNIQUEIDENTIFIER PRIMARY KEY,
	                [Name] NVARCHAR(128)
                )");
        }

        public static bool TableExists(this SqlConnection connection, string table = "SimpleTable")
        {
            var exists = connection.QueryFirstOrDefault<string>($"SELECT OBJECT_ID('{table}')");
            return !string.IsNullOrEmpty(exists);
        }
    }
}
