using System.Data.Entity.Migrations;

namespace Solab.EntityFramework.Tests.Migrations
{
    internal sealed class MigrationConfiguration : DbMigrationsConfiguration<Solab.EntityFramework.Tests.Context.DatabaseContext>
    {
        public MigrationConfiguration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("System.Data.SqlClient", new SolabMigrationGenerator());
        }

        protected override void Seed(Solab.EntityFramework.Tests.Context.DatabaseContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
