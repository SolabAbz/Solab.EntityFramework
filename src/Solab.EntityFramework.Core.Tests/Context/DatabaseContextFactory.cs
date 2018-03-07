using Microsoft.EntityFrameworkCore.Design;
using Solab.EntityFramework.Core.Tests.Helpers;

namespace Solab.EntityFramework.Core.Tests.Context
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            return DatabaseHelpers.CreateContext();
        }
    }
}