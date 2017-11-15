using NUnit.Framework;
using Solab.EntityFramework.Tests.Context;
using Solab.EntityFramework.Tests.Helpers;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace Solab.EntityFramework.Tests.MigrationTests
{
    [TestFixture]
    public class MigrationTests
    {
        [Test]
        public void MigrateDatabase()
        {
            var connectionString = GenerateConnectionString();

            using (var context = new DatabaseContext(connectionString))
            {
                context.People.Add(new Person { Id = Guid.NewGuid() });
                context.SaveChanges();
                var people = context.People.ToList();
                Assert.IsNotEmpty(people);
            }

            using(var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Assert.AreEqual(120, connection.CompatibilityLevel());
                Assert.IsTrue(connection.TableExists("People_History"));
            }
        }

        private string GenerateConnectionString()
        {
            var builder = new SqlConnectionStringBuilder(ConnectionFactory.DefaultLocalDbInstance)
            {
                InitialCatalog = "SolabEntityFrameworkTests",
                IntegratedSecurity = true
            };

            return builder.ToString();
        }
    }
}
