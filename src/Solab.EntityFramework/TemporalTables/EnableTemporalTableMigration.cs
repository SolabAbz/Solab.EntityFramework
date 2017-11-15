using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Utilities;
using System;

namespace Solab.EntityFramework.TemporalTables
{
    public class EnableTemporalTableMigration : MigrationOperation, IMigrationAction
    {
        public override bool IsDestructiveChange => false;

        public TemporalTableSettings Settings { get; }

        public Func<IndentedTextWriter> WriterDelegate { get; private set; }

        public Action<IndentedTextWriter> StatementDelegate { get; private set; }

        public EnableTemporalTableMigration(TemporalTableSettings settings,
            object anonymousArguments) : base(anonymousArguments)
        {
            Settings = settings;
        }

        public void Invoke(Func<IndentedTextWriter> writerDelegate, Action<IndentedTextWriter, bool> statementDelegate)
        {
            WriterDelegate = writerDelegate;
            StatementDelegate = x => statementDelegate.Invoke(x, false);
            AddPeriodColumns();
            AddDefaultValues();
            AddPeriodColumnConstraints();
            CreatePeriodIndex();
            EnableSystemVersioning();
        }

        private void AddPeriodColumns()
        {
            using (var writer = WriterDelegate.Invoke())
            {
                const string sql = "ALTER TABLE {0} ADD {1} [DATETIME2] NULL;";
                writer.WriteLine(sql, Settings.Table, Settings.StartPeriodColumnName);
                writer.WriteLine(sql, Settings.Table, Settings.EndPeriodColumnName);
                StatementDelegate.Invoke(writer);
            }
        }

        private void AddDefaultValues()
        {
            using (var writer = WriterDelegate.Invoke())
            {
                const string sql = "UPDATE {0} SET {1} = '{2}', {3} = '{4}';";
                writer.WriteLine(sql, Settings.Table, Settings.StartPeriodColumnName, "19000101 00:00:00.0000000", Settings.EndPeriodColumnName, "99991231 23:59:59.9999999");
                StatementDelegate.Invoke(writer);
            }
        }

        private void AddPeriodColumnConstraints()
        {
            using (var writer = WriterDelegate.Invoke())
            {
                const string sql = "ALTER TABLE {0} ALTER COLUMN {1} [DATETIME2] NOT NULL;";
                writer.WriteLine(sql, Settings.Table, Settings.StartPeriodColumnName);
                writer.WriteLine(sql, Settings.Table, Settings.EndPeriodColumnName);
                StatementDelegate.Invoke(writer);
            }
        }

        private void CreatePeriodIndex()
        {
            using (var writer = WriterDelegate.Invoke())
            {
                const string sql = "ALTER TABLE {0} ADD PERIOD FOR SYSTEM_TIME ({1}, {2});";
                writer.WriteLine(sql, Settings.Table, Settings.StartPeriodColumnName, Settings.EndPeriodColumnName);
                StatementDelegate.Invoke(writer);
            }
        }

        private void EnableSystemVersioning()
        {
            using (var writer = WriterDelegate.Invoke())
            {
                const string sql = "ALTER TABLE {0} SET(SYSTEM_VERSIONING = ON (HISTORY_TABLE = {1}, DATA_CONSISTENCY_CHECK = ON));";
                writer.WriteLine(sql, Settings.Table, Settings.HistoryTable);
                StatementDelegate.Invoke(writer);
            }
        }
    }
}
