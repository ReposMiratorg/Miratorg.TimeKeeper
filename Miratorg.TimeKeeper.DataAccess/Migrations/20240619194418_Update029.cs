using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Miratorg.TimeKeeper.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Update029 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "ManualScuds");

            migrationBuilder.DropColumn(
                name: "UserAutorName",
                table: "ManualScuds");

            migrationBuilder.CreateTable(
                name: "LogManualScuds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Input = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Output = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypeLog = table.Column<int>(type: "int", nullable: false),
                    Autor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogManualScuds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Begin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlanType = table.Column<int>(type: "int", nullable: false),
                    StoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomTypeWorkId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TypeOverWorkId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypeLog = table.Column<int>(type: "int", nullable: false),
                    Autor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogPlans", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogManualScuds");

            migrationBuilder.DropTable(
                name: "LogPlans");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "ManualScuds",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserAutorName",
                table: "ManualScuds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
