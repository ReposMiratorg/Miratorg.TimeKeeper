using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Miratorg.TimeKeeper.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Update017 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_TypeOverWork_TypeOverWorkId",
                table: "Plans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeOverWork",
                table: "TypeOverWork");

            migrationBuilder.RenameTable(
                name: "TypeOverWork",
                newName: "TypeOverWorks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeOverWorks",
                table: "TypeOverWorks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_TypeOverWorks_TypeOverWorkId",
                table: "Plans",
                column: "TypeOverWorkId",
                principalTable: "TypeOverWorks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_TypeOverWorks_TypeOverWorkId",
                table: "Plans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeOverWorks",
                table: "TypeOverWorks");

            migrationBuilder.RenameTable(
                name: "TypeOverWorks",
                newName: "TypeOverWork");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeOverWork",
                table: "TypeOverWork",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_TypeOverWork_TypeOverWorkId",
                table: "Plans",
                column: "TypeOverWorkId",
                principalTable: "TypeOverWork",
                principalColumn: "Id");
        }
    }
}
