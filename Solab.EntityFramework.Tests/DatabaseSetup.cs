using NUnit.Framework;
using Solab.EntityFramework.Tests;
using Solab.EntityFramework.Tests.Helpers;
#pragma warning disable RCS1110 // Declare type inside namespace.

[SetUpFixture]
public class DatabaseSetup
{
    [OneTimeSetUp]
    public void Setup()
    {
        using (var connection = ConnectionFactory.Create("master"))
        {
            if (connection.DatabaseExists(ConnectionFactory.DefaultDatabaseName))
            {
                connection.DropDatabase(ConnectionFactory.DefaultDatabaseName);
            }

            connection.CreateDatabase(ConnectionFactory.DefaultDatabaseName);
        }
    }

    [OneTimeTearDown]
    public void Teardown()
    {
        using (var connection = ConnectionFactory.Create("master"))
        {
            connection.DropDatabase(ConnectionFactory.DefaultDatabaseName);
        }
    }
}

#pragma warning restore RCS1110 // Declare type inside namespace.
