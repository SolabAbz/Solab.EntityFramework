using Solab.EntityFramework.DateTable;
using System;
using System.Data.Entity;

namespace Solab.EntityFramework.Tests.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        public DbSet<DateDimension> DateDimensions { get; set; }

        public DatabaseContext()
        {

        }

        public DatabaseContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<DatabaseContext>());
        }
    }

    public class Person
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
