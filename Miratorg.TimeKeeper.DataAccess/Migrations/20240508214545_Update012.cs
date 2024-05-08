using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Miratorg.TimeKeeper.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Update012 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_Stores_StoreId",
                table: "Plans");

            migrationBuilder.DropIndex(
                name: "IX_Plans_StoreId",
                table: "Plans");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Plans_StoreId",
                table: "Plans",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_Stores_StoreId",
                table: "Plans",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id");
        }
    }
}
