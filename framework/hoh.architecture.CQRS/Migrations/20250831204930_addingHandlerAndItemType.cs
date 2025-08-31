using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HoH.Architecture.CQRS.Migrations
{
    /// <inheritdoc />
    public partial class addingHandlerAndItemType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HandlerType",
                table: "CommandQueryExecutionLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ItemType",
                table: "CommandQueryExecutionLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HandlerType",
                table: "CommandQueryExecutionLogs");

            migrationBuilder.DropColumn(
                name: "ItemType",
                table: "CommandQueryExecutionLogs");
        }
    }
}
