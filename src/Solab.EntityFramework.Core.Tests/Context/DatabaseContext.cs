using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Solab.EntityFramework.Core.Tests.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(ConnectionFactory.ConnectionString)
                    .ReplaceService<IMigrationsSqlGenerator, SolabMigrationGenerator>();
            }
        }
    }

    public class Person
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
