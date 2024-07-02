using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Triarch.Database.Migrations
{
    /// <inheritdoc />
    public partial class Movereversedprogressionoptiontolevellabledata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reversed",
                table: "Progressions");

            migrationBuilder.AddColumn<bool>(
                name: "ProgressionReversed",
                table: "LevelableDefinitions",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProgressionReversed",
                table: "LevelableDefinitions");

            migrationBuilder.AddColumn<bool>(
                name: "Reversed",
                table: "Progressions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
