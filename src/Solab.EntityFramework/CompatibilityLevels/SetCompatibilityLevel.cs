using System;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Utilities;

namespace Solab.EntityFramework.CompatibilityLevels
{
    public class SetCompatibilityLevel : MigrationOperation, IMigrationAction
    {
        private readonly int compatibilityLevel;

        public Func<IndentedTextWriter> WriterDelegate { get; private set; }

        public Action<IndentedTextWriter, bool> StatementDelegate { get; private set; }

        public override bool IsDestructiveChange => false;

        public SetCompatibilityLevel(int compatibilityLevel) : base(null)
        {
            this.compatibilityLevel = compatibilityLevel;
        }

        public void Invoke(Func<IndentedTextWriter> writerDelegate, Action<IndentedTextWriter, bool> statementDelegate)
        {
            WriterDelegate = writerDelegate;
            StatementDelegate = statementDelegate;
            GenerateSql();
        }

        private void GenerateSql()
        {
            using (var writer = WriterDelegate.Invoke())
            {
                writer.WriteLine("DECLARE @name NVARCHAR(MAX) = '[' + DB_NAME() + ']';");
                writer.WriteLine($"EXEC('ALTER DATABASE ' + @name + ' SET COMPATIBILITY_LEVEL = {compatibilityLevel}');");
                StatementDelegate.Invoke(writer, true);
            }
        }
    }
}
