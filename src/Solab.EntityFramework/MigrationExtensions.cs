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
        /// <summary>
        ///     Enables temporal tables for a table.
        /// </summary>
        /// <param name="migration"></param>
        /// <param name="table">e.g. dbo.Person</param>
        /// <param name="startPeriodColumnName">
        ///     Overrides the start period default column name.  
        ///     You must also override the column name in the RemoveSystemVersioning call.
        /// </param>
        /// <param name="endPeriodColumnName">
        ///     Overrides the end period default column name.
        ///     You must also override the column name in the RemoveSystemVersioning call.
        /// </param>
        public static void EnableSystemVersioning(this DbMigration migration,
            string table,
            string startPeriodColumnName = "VersionStartTime",
            string endPeriodColumnName = "VersionEndTime")
        {
            ((IDbMigration)migration)
                .AddOperation(new EnableTemporalTable(
                    new TemporalTableSettings(table, startPeriodColumnName, endPeriodColumnName)));
        }

        /// <summary>
        ///     Disables temporal tables and deletes the history table.
        /// </summary>
        /// <param name="migration"></param>
        /// <param name="table">e.g. dbo.Person</param>
        /// <param name="startPeriodColumnName">
        ///     Overrides the start period default column name.
        ///     This should match your EnableSystemVersioning call.
        /// </param>
        /// <param name="endPeriodColumnName">
        ///     Overrides the end period default column name.
        ///     You must also override the column name in the RemoveSystemVersioning call.
        /// </param>
        public static void RemoveSystemVersioning(this DbMigration migration,
            string table,
            string startPeriodColumnName = "VersionStartTime",
            string endPeriodColumnName = "VersionEndTime")
        {
            ((IDbMigration)migration)
                .AddOperation(new RemoveTemporalTable(
                    new TemporalTableSettings(table, startPeriodColumnName, endPeriodColumnName)));
        }

        /// <summary>
        ///     Sets the compatibility level of a database.
        /// </summary>
        /// <param name="migration"></param>
        /// <param name="compatibilityLevel">The compatibility level to set.</param>
        public static void SetCompatibilityLevel(this DbMigration migration, int compatibilityLevel)
        {
            ((IDbMigration)migration)
                .AddOperation(new SetCompatibilityLevel(compatibilityLevel));
        }

        /// <summary>
        ///     Empties and then populates a DbSet<DateDimension> with data.
        /// </summary>
        /// <param name="migration"></param>
        /// <param name="table">The name of the table to populate e.g. dbo.DateDimension</param>
        /// <param name="start">The initial date to populate the table with</param>
        /// <param name="end">The final date to populate the table with</param>
        public static void PopulateDateTable(this DbMigration migration, string table, DateTime start, DateTime end)
        {
            ((IDbMigration)migration)
                .AddOperation(new PopulateDateTable(table, start, end));
        }
    }
}
