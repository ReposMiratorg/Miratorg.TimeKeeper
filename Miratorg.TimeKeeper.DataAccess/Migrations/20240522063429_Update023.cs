using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Miratorg.TimeKeeper.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Update023 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StoreLimits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Mouth = table.Column<int>(type: "int", nullable: false),
                    Limit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreLimits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreLimits_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreLimits_StoreId",
                table: "StoreLimits",
                column: "StoreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreLimits");
        }
    }
}
