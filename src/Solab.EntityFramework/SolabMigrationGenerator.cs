using System.Data.Entity.Migrations.Model;
using System.Data.Entity.SqlServer;

namespace Solab.EntityFramework
{
    public class SolabMigrationGenerator : SqlServerMigrationSqlGenerator
    {
        protected override void Generate(MigrationOperation migrationOperation)
        {
            if (migrationOperation is IMigrationAction operation)
            {
                operation.Invoke(Writer, (IndentedTextWriter, SuppressTransaction) =>
                {
                    if (SuppressTransaction)
                    {
                        Statement(IndentedTextWriter.InnerWriter.ToString(), suppressTransaction: SuppressTransaction);
                    }
                    else
                    {
                        Statement(IndentedTextWriter);
                    }
                });
            }
        }
    }
}
