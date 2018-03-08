using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace Solab.EntityFramework.Core
{
    public interface IMigrationAction
    {
        void Invoke(MigrationOperation operation, 
            IModel model, 
            MigrationCommandListBuilder builder, 
            ISqlGenerationHelper helper);
    }
}

