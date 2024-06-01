using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Miratorg.TimeKeeper.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Update025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PositionOverworks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeOverWorkEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionOverworks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PositionOverworks_TypeOverWorks_TypeOverWorkEntityId",
                        column: x => x.TypeOverWorkEntityId,
                        principalTable: "TypeOverWorks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PositionOverworks_TypeOverWorkEntityId",
                table: "PositionOverworks",
                column: "TypeOverWorkEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PositionOverworks");
        }
    }
}
