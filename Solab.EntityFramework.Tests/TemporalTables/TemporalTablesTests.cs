using NUnit.Framework;
using Solab.EntityFramework.TemporalTables;
using Solab.EntityFramework.Tests.Helpers;
using System.Data.Entity.Migrations.Utilities;
using System.IO;

namespace Solab.EntityFramework.Tests.TemporalTables
{
    [TestFixture]
    public class TemporalTablesTests
    {
        [Test]
        public void CreateTemporalTable()
        {
            var writer = new IndentedTextWriter(new StringWriter());
            var migration = new EnableTemporalTableMigration(new TemporalTableSettings("dbo.SimpleTable", "VersionStartTime", "VersionEndTime"), null);
            migration.Invoke(() => writer, (IndentedTextWriter) => { });
            var sql = writer.InnerWriter.ToString();

            using (var connection = ConnectionFactory.Create())
            {
                connection.CreateSimpleTable();
                connection.ExecuteBatch(sql);
                Assert.IsTrue(connection.TableExists());
                Assert.IsTrue(connection.TableExists("SimpleTable_History"));
            }
        }
    }
}
