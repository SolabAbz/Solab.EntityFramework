using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace Solab.EntityFramework.Core.TemporalTables
{
    public class EnableTemporalTable : MigrationOperation, IMigrationAction
    {
        public override bool IsDestructiveChange => false;

        public TemporalTableSettings Settings { get; }

        public MigrationCommandListBuilder Builder { get; set; }

        public ISqlGenerationHelper Helper { get; set; }

        public EnableTemporalTable(TemporalTableSettings settings)
        {
            Settings = settings;
        }

        public void Invoke(MigrationOperation operation,
            IModel model,
            MigrationCommandListBuilder builder,
            ISqlGenerationHelper helper)
        {
            Helper = helper;

            if (Settings.CreatePeriodColumns)
            {
                AddPeriodColumns(builder);
            }

            AddDefaultValues(builder);
            AddPeriodColumnConstraints(builder);
            CreatePeriodIndex(builder);
            EnableSystemVersioning(builder);
        }

        private void AddPeriodColumns(MigrationCommandListBuilder builder)
        {
            builder.Append("ALTER TABLE ")
                   .Append(Settings.Table)
                   .Append(" ADD ")
                   .Append(Settings.StartPeriodColumnName)
                   .Append(" [DATETIME2] NULL;")
                   .EndCommand(true);

            builder.Append("ALTER TABLE ")
                   .Append(Settings.Table)
                   .Append(" ADD ")
                   .Append(Settings.EndPeriodColumnName)
                   .Append(" [DATETIME2] NULL;")
                   .EndCommand(true);
        }

        private void AddDefaultValues(MigrationCommandListBuilder builder)
        {
            builder.Append("UPDATE ")
                   .Append(Settings.Table)
                   .Append(" SET ")
                   .Append(Settings.StartPeriodColumnName)
                   .Append(" = '")
                   .Append(Helper.EscapeLiteral("19000101 00:00:00.0000000"))
                   .Append("', ")
                   .Append(Settings.EndPeriodColumnName)
                   .Append(" = '")
                   .Append(Helper.EscapeLiteral("99991231 23:59:59.9999999"))
                   .Append("';")
                   .EndCommand(true);
        }

        private void AddPeriodColumnConstraints(MigrationCommandListBuilder builder)
        {
            builder.Append("ALTER TABLE ")
                   .Append(Settings.Table)
                   .Append(" ALTER COLUMN ")
                   .Append(Settings.StartPeriodColumnName)
                   .Append(" [DATETIME2] NOT NULL;")
                   .EndCommand(true);

            builder.Append("ALTER TABLE ")
                    .Append(Settings.Table)
                    .Append(" ALTER COLUMN ")
                    .Append(Settings.EndPeriodColumnName)
                    .Append(" [DATETIME2] NOT NULL;")
                    .EndCommand(true);
        }

        private void CreatePeriodIndex(MigrationCommandListBuilder builder)
        {
            builder.Append("ALTER TABLE ")
                   .Append(Settings.Table)
                   .Append(" ADD PERIOD FOR SYSTEM_TIME (")
                   .Append(Helper.EscapeLiteral(Settings.StartPeriodColumnName))
                   .Append(",")
                   .Append(Helper.EscapeLiteral(Settings.EndPeriodColumnName))
                   .Append(");")
                   .EndCommand(true);
        }

        private void EnableSystemVersioning(MigrationCommandListBuilder builder)
        {
            builder.Append("ALTER TABLE ")
                   .Append(Settings.Table)
                   .Append("SET(SYSTEM_VERSIONING = ON (HISTORY_TABLE = ")
                   .Append(Helper.EscapeLiteral(Settings.HistoryTable))
                   .Append(", DATA_CONSISTENCY_CHECK = ON));")
                   .EndCommand(true);
        }
    }
}
