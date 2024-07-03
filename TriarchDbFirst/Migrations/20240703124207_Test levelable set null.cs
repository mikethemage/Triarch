using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TriarchDbFirst.Migrations
{
    /// <inheritdoc />
    public partial class Testlevelablesetnull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RpgelementDefinitions_LevelableDefinitions_LevelableDataId",
                table: "RpgelementDefinitions");

            migrationBuilder.AddForeignKey(
                name: "FK_RpgelementDefinitions_LevelableDefinitions_LevelableDataId",
                table: "RpgelementDefinitions",
                column: "LevelableDataId",
                principalTable: "LevelableDefinitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RpgelementDefinitions_LevelableDefinitions_LevelableDataId",
                table: "RpgelementDefinitions");

            migrationBuilder.AddForeignKey(
                name: "FK_RpgelementDefinitions_LevelableDefinitions_LevelableDataId",
                table: "RpgelementDefinitions",
                column: "LevelableDataId",
                principalTable: "LevelableDefinitions",
                principalColumn: "Id");
        }
    }
}
