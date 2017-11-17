namespace Solab.EntityFramework.Tests.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddedDateDimension : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DateDimensions",
                c => new
                {
                    Date = c.DateTime(nullable: false),
                    Year = c.Int(nullable: false),
                    Month = c.Int(nullable: false),
                    Day = c.Int(nullable: false),
                    DayOfWeek = c.Int(nullable: false),
                    DayOfYear = c.Int(nullable: false),
                    DayOfWeekName = c.String(maxLength: 16),
                    MonthName = c.String(maxLength: 16),
                    Week = c.Int(nullable: false),
                    Suffix = c.String(maxLength: 8),
                })
                .PrimaryKey(t => t.Date);

            this.PopulateDateTable("dbo.DateDimensions", new DateTime(2000, 1, 1), new DateTime(2020, 12, 31));
        }

        public override void Down()
        {
            DropTable("dbo.DateDimensions");
        }
    }
}
