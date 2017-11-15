using Dapper;
using NUnit.Framework;
using Solab.EntityFramework.CompatibilityLevels;
using Solab.EntityFramework.Tests.Helpers;
using System.Data.Entity.Migrations.Utilities;
using System.IO;

namespace Solab.EntityFramework.Tests.CompatibilityLevels
{
    [TestFixture]
    public class SetCompatibilityLevelTests
    {
        [Test]
        public void Set_Compatibility_Level()
        {
            var writer = TextWriterHelpers.Create();
            var migration = new SetCompatibilityLevel(120, null);
            migration.Invoke(() => writer, (IndentedTextWriter) => { });
            var sql = writer.InnerWriter.ToString();

            using (var connection = ConnectionFactory.Create())
            {
                Assert.AreNotEqual(120, connection.CompatibilityLevel());
                connection.CreateSimpleTable();
                connection.Execute(sql);
                Assert.AreEqual(120, connection.CompatibilityLevel());
            }
        }
    }
}
