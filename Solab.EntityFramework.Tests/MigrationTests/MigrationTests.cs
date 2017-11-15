using NUnit.Framework;
using Solab.EntityFramework.Tests.Context;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solab.EntityFramework.Tests.MigrationTests
{
    [TestFixture]
    public class MigrationTests
    {
        [Test]
        public void MigrateDatabase()
        {
            using (var context = new DatabaseContext(GenerateConnectionString()))
            {
                context.People.Add(new Person { Id = Guid.NewGuid() });
                context.SaveChanges();
            }

            using (var context = new DatabaseContext(ConnectionFactory.DefaultLocalDbInstance))
            {
                var people = context.People.ToList();
                Assert.IsNotEmpty(people);
            }
        }

        private string GenerateConnectionString()
        {
            var builder = new SqlConnectionStringBuilder(ConnectionFactory.DefaultLocalDbInstance);
            builder.InitialCatalog = "SolabEntityFrameworkTests";
            return builder.ToString();
        }
    }
}
