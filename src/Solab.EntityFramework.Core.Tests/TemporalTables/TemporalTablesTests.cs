using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Solab.EntityFramework.Core.Tests.Context;
using Solab.EntityFramework.Core.Tests.Helpers;
using System;
using System.Linq;

namespace Solab.EntityFramework.Core.Tests.TemporalTables
{
    [TestFixture]
    public class TemporalTablesTests
    {
        /// <summary>
        /// Ensures that temporal tables are created & System versioning is enabled
        /// </summary>
        [Test]
        public void CreateTemporalTable()
        {
            using(var context = DatabaseHelpers.CreateContext())
            {
                // Arrange/Act
                var id = Guid.NewGuid();

                context.People.Add(new Context.Person
                {
                    Id = id,
                    Name = "Dazza"
                });

                context.SaveChanges();

                var person = context.People.SingleOrDefault(x => x.Id == id);
                person.Name = "MadDawg";

                context.SaveChanges();

                // Assert
                var historic = context.People.FromSql("SELECT * FROM [dbo].[People_History]").ToList();
                Assert.IsTrue(context.People.Any());
                Assert.IsTrue(historic.Any());
            }
        }

        /// <summary>
        /// Table should have been created then removed by migrations
        /// </summary>
        [Test]
        public void RemoveTemporalTable()
        {
            // Arrange/Act
            using (var connection = ConnectionFactory.Create())
            {
                //Assert
                Assert.IsTrue(connection.TableExists("Invoices"));
                Assert.IsFalse(connection.TableExists("Invoices_History"));
            }
        }
    }
}
