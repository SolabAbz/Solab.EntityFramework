namespace Solab.EntityFramework.Tests.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            this.EnableSystemVersioning("dbo.People");
            this.SetCompatibilityLevel(120);
        }

        public override void Down()
        {
            DropTable("dbo.People");

            this.RemoveSystemVersioning("dbo.People");
            this.SetCompatibilityLevel(140);
        }
    }
}
