using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Solab.EntityFramework.Core.Tests.Migrations
{
    public partial class RemovedInvoiceTemporalTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RemoveSystemVersioning("Invoices");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnableSystemVersioning("Invoices");
        }
    }
}
