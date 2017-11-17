namespace Solab.EntityFramework.Tests.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPersonName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "Name");
        }
    }
}
