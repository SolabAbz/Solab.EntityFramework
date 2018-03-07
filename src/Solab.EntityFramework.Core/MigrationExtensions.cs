using Microsoft.EntityFrameworkCore.Migrations;
using Solab.EntityFramework.Core.TemporalTables;
using System;

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
        public static void EnableSystemVersioning(this MigrationBuilder migration,
            string table,
            string schema = "dbo",
            string startPeriodColumnName = "VersionStartTime",
            string endPeriodColumnName = "VersionEndTime",
            bool createPeriodColumns = true)
        {
            var settings = new TemporalTableSettings(table, startPeriodColumnName, endPeriodColumnName, createPeriodColumns, schema);
            migration.Operations.Add(new EnableTemporalTable(settings));
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
        public static void RemoveSystemVersioning(this MigrationBuilder migration,
            string table,
            string schema = "dbo",
            string startPeriodColumnName = "VersionStartTime",
            string endPeriodColumnName = "VersionEndTime",
            bool createPeriodColumns = true)
        {
            var settings = new TemporalTableSettings(table, startPeriodColumnName, endPeriodColumnName, createPeriodColumns, schema);
            migration.Operations.Add(new EnableTemporalTable(settings));
        }
    }
}
