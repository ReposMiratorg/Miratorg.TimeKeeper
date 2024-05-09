using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Miratorg.TimeKeeper.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Update013 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CustomTypeWorkId",
                table: "Plans",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CustomTypeWorks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomTypeWorks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plans_CustomTypeWorkId",
                table: "Plans",
                column: "CustomTypeWorkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_CustomTypeWorks_CustomTypeWorkId",
                table: "Plans",
                column: "CustomTypeWorkId",
                principalTable: "CustomTypeWorks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_CustomTypeWorks_CustomTypeWorkId",
                table: "Plans");

            migrationBuilder.DropTable(
                name: "CustomTypeWorks");

            migrationBuilder.DropIndex(
                name: "IX_Plans_CustomTypeWorkId",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "CustomTypeWorkId",
                table: "Plans");
        }
    }
}
