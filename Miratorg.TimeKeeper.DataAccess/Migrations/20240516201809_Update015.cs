using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Miratorg.TimeKeeper.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Update015 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TypeOverWorkId",
                table: "Plans",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TypeOverWork",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOverWork", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plans_TypeOverWorkId",
                table: "Plans",
                column: "TypeOverWorkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_TypeOverWork_TypeOverWorkId",
                table: "Plans",
                column: "TypeOverWorkId",
                principalTable: "TypeOverWork",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_TypeOverWork_TypeOverWorkId",
                table: "Plans");

            migrationBuilder.DropTable(
                name: "TypeOverWork");

            migrationBuilder.DropIndex(
                name: "IX_Plans_TypeOverWorkId",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "TypeOverWorkId",
                table: "Plans");
        }
    }
}
