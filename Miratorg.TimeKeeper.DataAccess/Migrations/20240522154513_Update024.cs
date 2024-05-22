using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Miratorg.TimeKeeper.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Update024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HourLimit",
                table: "Stores");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HourLimit",
                table: "Stores",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
