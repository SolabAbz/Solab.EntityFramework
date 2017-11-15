using System;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Utilities;

namespace Solab.EntityFramework.CompatibilityLevels
{
    public class SetCompatibilityLevel : MigrationOperation, IMigrationAction
    {
        private readonly int compatibilityLevel;

        public Func<IndentedTextWriter> WriterDelegate { get; private set; }

        public override bool IsDestructiveChange => false;

        public SetCompatibilityLevel(int compatibilityLevel, object anonymousArguments) : base(anonymousArguments)
        {
            this.compatibilityLevel = compatibilityLevel;
        }

        public void Invoke(Func<IndentedTextWriter> writerDelegate, Action<IndentedTextWriter> statementDelegate)
        {
            WriterDelegate = writerDelegate;
            DeclareName();
            AlterDatabase();
        }

        private void DeclareName()
        {
            using (var writer = WriterDelegate.Invoke())
            {
                writer.WriteLine("DECLARE @name NVARCHAR(MAX) = '[' + DB_NAME() + ']';");
            }
        }

        private void AlterDatabase()
        {
            using(var writer = WriterDelegate.Invoke())
            {
                writer.WriteLine($"EXEC('ALTER DATABASE ' + @name + ' SET COMPATIBILITY_LEVEL = {compatibilityLevel}');");
            }
        }
    }
}
