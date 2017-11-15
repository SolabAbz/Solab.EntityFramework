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
            var writer = new IndentedTextWriter(new StringWriter());
            var migration = new SetCompatibilityLevel(120, null);
            migration.Invoke(() => writer, (IndentedTextWriter) => { });
            var sql = writer.InnerWriter.ToString();

            using (var connection = ConnectionFactory.Create())
            {
                var level = connection.QueryFirst<int>("select compatibility_level from sys.databases where name = DB_NAME()");
                Assert.AreNotEqual(120, level);

                connection.CreateSimpleTable();
                connection.Execute(sql);

                level = connection.QueryFirst<int>("select compatibility_level from sys.databases where name = DB_NAME()");
                Assert.AreEqual(120, level);
            }
        }
    }
}
