using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TriarchDbFirst.Migrations
{
    /// <inheritdoc />
    public partial class TestCascadeFreebiev2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rpgfreebies_RpgelementDefinitions_FreebieElementDefinitionId",
                table: "Rpgfreebies");

            migrationBuilder.AddForeignKey(
                name: "FK_Rpgfreebies_RpgelementDefinitions_FreebieElementDefinitionId",
                table: "Rpgfreebies",
                column: "FreebieElementDefinitionId",
                principalTable: "RpgelementDefinitions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rpgfreebies_RpgelementDefinitions_FreebieElementDefinitionId",
                table: "Rpgfreebies");

            migrationBuilder.AddForeignKey(
                name: "FK_Rpgfreebies_RpgelementDefinitions_FreebieElementDefinitionId",
                table: "Rpgfreebies",
                column: "FreebieElementDefinitionId",
                principalTable: "RpgelementDefinitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
