using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Miratorg.TimeKeeper.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Update005 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Schedules_ScheduleEntityId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ScheduleEntityId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ScheduleEntityId",
                table: "Employees");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ScheduleId",
                table: "Employees",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Schedules_ScheduleId",
                table: "Employees",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Schedules_ScheduleId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ScheduleId",
                table: "Employees");

            migrationBuilder.AddColumn<Guid>(
                name: "ScheduleEntityId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ScheduleEntityId",
                table: "Employees",
                column: "ScheduleEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Schedules_ScheduleEntityId",
                table: "Employees",
                column: "ScheduleEntityId",
                principalTable: "Schedules",
                principalColumn: "Id");
        }
    }
}
