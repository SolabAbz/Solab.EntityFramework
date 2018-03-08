using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Solab.EntityFramework.Core
{
    public class SolabMigrationGenerator : SqlServerMigrationsSqlGenerator
    {
        public SolabMigrationGenerator(
            MigrationsSqlGeneratorDependencies dependencies, 
            IMigrationsAnnotationProvider migrationsAnnotations) 
            : base(dependencies, migrationsAnnotations)
        {

        }

        protected override void Generate(
            MigrationOperation operation,
            IModel model,
            MigrationCommandListBuilder builder)
        {
            if (operation is IMigrationAction customOperation)
            {
                customOperation.Invoke(operation, model, builder, Dependencies.SqlGenerationHelper);
            }
            else
            {
                base.Generate(operation, model, builder);
            }
        }
    }
}
