using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Triarch.Database.Migrations
{
    /// <inheritdoc />
    public partial class Addbuiltinoptionfortypessowecanhavecharactertype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "BuiltIn",
                table: "RPGElementTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "CostPerLevel",
                table: "LevelableDefinitions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_RPGElementDefinitions_ElementTypeId",
                table: "RPGElementDefinitions",
                column: "ElementTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_RPGElementDefinitions_RPGElementTypes_ElementTypeId",
                table: "RPGElementDefinitions",
                column: "ElementTypeId",
                principalTable: "RPGElementTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RPGElementDefinitions_RPGElementTypes_ElementTypeId",
                table: "RPGElementDefinitions");

            migrationBuilder.DropIndex(
                name: "IX_RPGElementDefinitions_ElementTypeId",
                table: "RPGElementDefinitions");

            migrationBuilder.DropColumn(
                name: "BuiltIn",
                table: "RPGElementTypes");

            migrationBuilder.AlterColumn<int>(
                name: "CostPerLevel",
                table: "LevelableDefinitions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
