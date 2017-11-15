using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solab.EntityFramework.Tests.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        public DatabaseContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<DatabaseContext>());
        }
    }

    public class Person
    {
        public Guid Id { get; set; }
    }
}
