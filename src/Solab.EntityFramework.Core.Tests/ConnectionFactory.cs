using System.Data.SqlClient;

namespace Solab.EntityFramework.Core.Tests
{
    public static class ConnectionFactory
    {
        public const string DefaultLocalDbInstance = "Data Source=(LocalDB)\\MSSQLLocalDB";
        public const string DefaultDatabaseName = "UnitTests";
        public static string ConnectionString = $"{DefaultLocalDbInstance};Database={DefaultDatabaseName}";

        public static SqlConnection Create(string database = DefaultDatabaseName)
        {
            var connection = new SqlConnection(DefaultLocalDbInstance);
            connection.Open();
            connection.ChangeDatabase(database);
            return connection;
        }
    }
}
