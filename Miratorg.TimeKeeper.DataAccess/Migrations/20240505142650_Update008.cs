using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Miratorg.TimeKeeper.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Update008 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateInput",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "DateOutput",
                table: "Plans");

            migrationBuilder.RenameColumn(
                name: "DateKey",
                table: "Plans",
                newName: "End");

            migrationBuilder.AddColumn<DateTime>(
                name: "Begin",
                table: "Plans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Begin",
                table: "Plans");

            migrationBuilder.RenameColumn(
                name: "End",
                table: "Plans",
                newName: "DateKey");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateInput",
                table: "Plans",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOutput",
                table: "Plans",
                type: "datetime2",
                nullable: true);
        }
    }
}
