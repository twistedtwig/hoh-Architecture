using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HoH.Architecture.CQRS.Migrations
{
    /// <inheritdoc />
    public partial class extraLoggingFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Error",
                table: "CommandQueryExecutionLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ItemJson",
                table: "CommandQueryExecutionLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Success",
                table: "CommandQueryExecutionLogs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeSpan",
                table: "CommandQueryExecutionLogs",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "CommandQueryExecutionLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Error",
                table: "CommandQueryExecutionLogs");

            migrationBuilder.DropColumn(
                name: "ItemJson",
                table: "CommandQueryExecutionLogs");

            migrationBuilder.DropColumn(
                name: "Success",
                table: "CommandQueryExecutionLogs");

            migrationBuilder.DropColumn(
                name: "TimeSpan",
                table: "CommandQueryExecutionLogs");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "CommandQueryExecutionLogs");
        }
    }
}
