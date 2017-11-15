using System;
using System.Data.Entity.Migrations.Utilities;

namespace Solab.EntityFramework
{
    public interface IMigrationAction
    {
        void Invoke(Func<IndentedTextWriter> writerDelegate, Action<IndentedTextWriter, bool> statementDelegate);
    }
}

