# Solab Entity Framework

Solab entity framework is a small libary that extends the default entity framework 6 & ef core migration generators with support for temporal tables and creation of date dimensions.

## Packages
| Package Name               	| Minimum Versions                             	|                                 	|
|----------------------------	|----------------------------------------------	|---------------------------------	|
| Solab.EntityFramework      	| EF 6+                                        	| [Nuget](https://www.google.com) 	|
| Solab.EntityFramework.Core 	| Microsoft.EntityFrameworkCore.SqlServer 2.1+ 	| [Nuget](https://www.google.com) 	|


## Configuration

## EF6

```csharp
    using Solab.EntityFramework;
    using System.Data.Entity.Migrations;
    using System.Linq;

    namespace Example.Infrastructure.Migrations
    {
        public sealed class Configuration : DbMigrationsConfiguration<ApplicationContext>
        {
            public Configuration()
            {
                AutomaticMigrationsEnabled = false;
                SetSqlGenerator("System.Data.SqlClient", new SolabMigrationGenerator());
            }
        }
    }
```

## EF Core

```csharp
    using Solab.EntityFramework.Core;

    ...

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ApplicationContext>(options => options
            .ReplaceService<IMigrationsSqlGenerator, SolabMigrationGenerator>()
            .UseSqlServer("Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;"));
    }

```

## Examples
### EF6 - Temporal Tables
```csharp
    public partial class EnableTemporalTablesOnPeople : DbMigration
    {
        public override void Up()
        {
            this.EnableSystemVersioning("dbo.People");
        }

        public override void Down()
        {
            this.RemoveSystemVersioning("dbo.People");
        }
    }
```

### EF Core - Temporal Tables

```csharp
using Microsoft.EntityFrameworkCore.Migrations;
using Solab.EntityFramework;
using System;
using System.Collections.Generic;

namespace Example.Migrations
{
    public partial class EnabledTemporalTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnableSystemVersioning("People");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RemoveSystemVersioning("People");
        }
    }
}
```