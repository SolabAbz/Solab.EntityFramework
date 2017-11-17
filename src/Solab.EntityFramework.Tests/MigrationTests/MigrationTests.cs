using NUnit.Framework;
using Solab.EntityFramework.Tests.Context;
using Solab.EntityFramework.Tests.Helpers;
using System;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using Dapper;
using Solab.EntityFramework.DateTable;
using System.Data.Entity.Migrations;
using Solab.EntityFramework.Tests.Migrations;

namespace Solab.EntityFramework.Tests.MigrationTests
{
    [TestFixture]
    public class MigrationTests
    {
        private readonly string connectionString;

        public MigrationTests()
        {
            var builder = new SqlConnectionStringBuilder(ConnectionFactory.DefaultLocalDbInstance)
            {
                InitialCatalog = "SolabEntityFrameworkTests",
                IntegratedSecurity = true
            };

            connectionString = builder.ToString();
        }

        [OneTimeSetUp]
        public void Setup()
        {
            using (var context = new DatabaseContext(connectionString))
            {
                context.Database.CreateIfNotExists();

                var migrator = new DbMigrator(new DbMigrationsConfiguration
                {
                    TargetDatabase = new DbConnectionInfo(connectionString, "System.Data.SqlClient"),
                    ContextType = typeof(DatabaseContext),
                    ContextKey = typeof(MigrationConfiguration).FullName,
                    MigrationsAssembly = typeof(TestContext).Assembly,
                    MigrationsNamespace = typeof(MigrationConfiguration).Namespace
                });

                migrator.Update();
            }
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            using (var context = new DatabaseContext(connectionString))
            {
                context.Database.Delete();
            }
        }

        [Test]
        public void Ensure_History_Table_Created()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var id = Guid.NewGuid();
                connection.Execute("INSERT INTO dbo.People (id, name) VALUES (@id, @name)", new { id, name = "Peter" });
                connection.Execute("UPDATE dbo.People SET Name = @name where id = @id", new { id, name = "Sam" });
                var records = connection.QueryFirst<int>("SELECT COUNT(*) from dbo.People_History");

                Assert.IsTrue(connection.TableExists("People_History"));
                Assert.Greater(records, 0);
                Assert.AreEqual(120, connection.CompatibilityLevel());
            }
        }

        [Test]
        public void Ensure_Date_Dimension_Table_Populated()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var count = connection.QueryFirst<int>("SELECT COUNT(*) FROM dbo.DateDimensions");
                Assert.Greater(count, 0);
            }
        }
    }
}
