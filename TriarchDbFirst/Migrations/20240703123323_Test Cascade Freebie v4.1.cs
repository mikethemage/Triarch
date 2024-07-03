using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TriarchDbFirst.Migrations
{
    /// <inheritdoc />
    public partial class TestCascadeFreebiev41 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rpgfreebies_RpgelementDefinitions_OwnerElementDefinitionId",
                table: "Rpgfreebies");

            migrationBuilder.AddForeignKey(
                name: "FK_Rpgfreebies_RpgelementDefinitions_OwnerElementDefinitionId",
                table: "Rpgfreebies",
                column: "OwnerElementDefinitionId",
                principalTable: "RpgelementDefinitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rpgfreebies_RpgelementDefinitions_OwnerElementDefinitionId",
                table: "Rpgfreebies");

            migrationBuilder.AddForeignKey(
                name: "FK_Rpgfreebies_RpgelementDefinitions_OwnerElementDefinitionId",
                table: "Rpgfreebies",
                column: "OwnerElementDefinitionId",
                principalTable: "RpgelementDefinitions",
                principalColumn: "Id");
        }
    }
}
