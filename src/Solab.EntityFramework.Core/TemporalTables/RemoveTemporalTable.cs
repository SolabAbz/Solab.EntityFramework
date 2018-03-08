using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace Solab.EntityFramework.Core.TemporalTables
{
    public class RemoveTemporalTable : MigrationOperation, IMigrationAction
    {
        public override bool IsDestructiveChange => true;

        public TemporalTableSettings Settings { get; }

        public MigrationCommandListBuilder Builder { get; set; }

        public ISqlGenerationHelper Helper { get; set; }

        public RemoveTemporalTable(TemporalTableSettings settings)
        {
            Settings = settings;
        }

        public void Invoke(MigrationOperation operation,
            IModel model,
            MigrationCommandListBuilder builder,
            ISqlGenerationHelper helper)
        {
            Helper = helper;

            DisableSystemVersioning(builder);
            DropPeriodConstraint(builder);

            if (Settings.CreatePeriodColumns)
            {
                DropPeriodColumns(builder);
            }

            DropHistoryTable(builder);
        }

        private void DisableSystemVersioning(MigrationCommandListBuilder builder)
        {
            builder.Append("ALTER TABLE ")
                   .Append(Settings.Table)
                   .Append(" SET (SYSTEM_VERSIONING = OFF)")
                   .EndCommand(true);
        }

        private void DropPeriodColumns(MigrationCommandListBuilder builder)
        {
            builder.Append("ALTER TABLE ")
                   .Append(Settings.Table)
                   .Append(" DROP COLUMN ")
                   .Append(Settings.StartPeriodColumnName)
                   .EndCommand(true);

            builder.Append("ALTER TABLE ")
                   .Append(Settings.Table)
                   .Append(" DROP COLUMN ")
                   .Append(Settings.EndPeriodColumnName)
                   .EndCommand(true);
        }

        private void DropPeriodConstraint(MigrationCommandListBuilder builder)
        {
            builder.Append("ALTER TABLE ")
                   .Append(Settings.Table)
                   .Append(" DROP PERIOD FOR SYSTEM_TIME")
                   .EndCommand(true);
        }

        private void DropHistoryTable(MigrationCommandListBuilder builder)
        {
            builder.Append("DROP TABLE ")
                   .Append(Settings.HistoryTable)
                   .EndCommand(true);
        }

    }
}
