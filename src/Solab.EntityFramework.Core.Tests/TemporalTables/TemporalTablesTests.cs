using NUnit.Framework;
using Solab.EntityFramework.Core.Tests.Helpers;
using System;
using System.Linq;

namespace Solab.EntityFramework.Core.Tests.TemporalTables
{
    [TestFixture]
    public class TemporalTablesTests
    {
        [Test]
        public void CreateTemporalTable()
        {
            var context = DatabaseHelpers.CreateContext();

            context.People.Add(new Context.Person
            {
                Id = Guid.NewGuid(),
                Name = "Dazza"
            });

            context.SaveChanges();

            context = DatabaseHelpers.CreateContext();

            //Force init
            var people = context.People.ToList();
            Assert.That(people.Count > 0);

        }
    }
}
