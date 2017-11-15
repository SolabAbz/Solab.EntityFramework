using Solab.EntityFramework.TemporalTables;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Utilities;
using System.Data.Entity.SqlServer;

namespace Solab.EntityFramework
{
    public static class MigrationExtensions
    {
        public static void EnableSystemVersioning(this DbMigration migration,
            string table,
            string startPeriodColumnName = "VersionStartTime",
            string endPeriodColumnName = "VersionEndTime")
        {
            ((IDbMigration)migration)
                .AddOperation(new EnableTemporalTableMigration(
                    new TemporalTableSettings(table, startPeriodColumnName, endPeriodColumnName), null));
        }

        public static void RemoveSystemVersioning(this DbMigration migration,
            string table,
            string startPeriodColumnName = "VersionStartTime",
            string endPeriodColumnName = "VersionEndTime")
        {
            ((IDbMigration)migration)
                .AddOperation(new RemoveTemporalTableMigration(
                    new TemporalTableSettings(table, startPeriodColumnName, endPeriodColumnName), null));
        }
    }
}
