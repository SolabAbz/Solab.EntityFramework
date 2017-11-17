using Solab.EntityFramework.CompatibilityLevels;
using Solab.EntityFramework.DateTable;
using Solab.EntityFramework.TemporalTables;
using System;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;

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
                .AddOperation(new EnableTemporalTable(
                    new TemporalTableSettings(table, startPeriodColumnName, endPeriodColumnName)));
        }

        public static void RemoveSystemVersioning(this DbMigration migration,
            string table,
            string startPeriodColumnName = "VersionStartTime",
            string endPeriodColumnName = "VersionEndTime")
        {
            ((IDbMigration)migration)
                .AddOperation(new RemoveTemporalTable(
                    new TemporalTableSettings(table, startPeriodColumnName, endPeriodColumnName)));
        }

        public static void SetCompatibilityLevel(this DbMigration migration, int compatibilityLevel)
        {
            ((IDbMigration)migration)
                .AddOperation(new SetCompatibilityLevel(compatibilityLevel));
        }

        public static void PopulateDateTable(this DbMigration migration, string table, DateTime start, DateTime end)
        {
            ((IDbMigration)migration)
                .AddOperation(new PopulateDateTable(table, start, end));
        }
    }
}
