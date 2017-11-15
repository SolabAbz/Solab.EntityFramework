using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solab.EntityFramework.Tests.Migrations.Configuration
{
    public class CustomSqlServerMigrationGenerator : SqlServerMigrationSqlGenerator
    {
        protected override void Generate(MigrationOperation migrationOperation)
        {
            if (migrationOperation is IMigrationAction operation)
            {
                operation.Invoke(Writer, (IndentedTextWriter) => Statement(IndentedTextWriter));
            }
        }
    }
}
