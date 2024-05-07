using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Miratorg.TimeKeeper.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Update010 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StoreId",
                table: "Plans",
                type: "uniqueidentifier",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_Stores_StoreId",
                table: "Plans");

            migrationBuilder.DropIndex(
                name: "IX_Plans_StoreId",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Plans");
        }
    }
}
