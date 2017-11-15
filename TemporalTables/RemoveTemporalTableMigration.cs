using System;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Utilities;

namespace Solab.EntityFramework.TemporalTables
{
    public class RemoveTemporalTableMigration : MigrationOperation, IMigrationAction
    {
        public override bool IsDestructiveChange => true;

        public TemporalTableSettings Settings { get; }

        public Func<IndentedTextWriter> WriterDelegate { get; private set; }

        public Action<IndentedTextWriter> StatementDelegate { get; private set; }

        public RemoveTemporalTableMigration(TemporalTableSettings settings,
            object anonymousArguments) : base(anonymousArguments)
        {
            Settings = settings;
        }

        public void Invoke(Func<IndentedTextWriter> writerDelegate, Action<IndentedTextWriter, bool> statementDelegate)
        {
            WriterDelegate = writerDelegate;
            StatementDelegate = x => statementDelegate.Invoke(x, false);
            DisableSystemVersioning();
            DropPeriodConstraint();
            DropPeriodColumns();
            DropHistoryTable();
        }

        private void DisableSystemVersioning()
        {
            using (var writer = WriterDelegate.Invoke())
            {
                const string sql = "ALTER TABLE {0} SET(SYSTEM_VERSIONING = OFF);";
                writer.WriteLine(sql, Settings.Table);
                StatementDelegate.Invoke(writer);
            }
        }

        private void DropPeriodColumns()
        {
            using (var writer = WriterDelegate.Invoke())
            {
                const string sql = "ALTER TABLE {0} DROP COLUMN {1};";
                writer.WriteLine(sql, Settings.Table, Settings.StartPeriodColumnName);
                writer.WriteLine(sql, Settings.Table, Settings.EndPeriodColumnName);
                StatementDelegate.Invoke(writer);
            }
        }

        private void DropPeriodConstraint()
        {
            using (var writer = WriterDelegate.Invoke())
            {
                const string sql = "ALTER TABLE {0} DROP PERIOD FOR SYSTEM_TIME;";
                writer.WriteLine(sql, Settings.Table);
                StatementDelegate.Invoke(writer);
            }
        }

        private void DropHistoryTable()
        {
            using (var writer = WriterDelegate.Invoke())
            {
                const string sql = "DROP TABLE {0};";
                writer.WriteLine(sql, Settings.HistoryTable);
                StatementDelegate.Invoke(writer);
            }
        }
    }
}
