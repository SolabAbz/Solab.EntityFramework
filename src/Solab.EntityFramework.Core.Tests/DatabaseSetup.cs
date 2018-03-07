using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Solab.EntityFramework.Core.Tests;
using Solab.EntityFramework.Core.Tests.Helpers;
using System;

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

            var context = DatabaseHelpers.CreateContext();
            context.Database.Migrate();
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

