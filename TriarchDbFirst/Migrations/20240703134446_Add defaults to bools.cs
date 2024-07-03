using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TriarchDbFirst.Migrations
{
    /// <inheritdoc />
    public partial class Adddefaultstobools : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Progressions_RPGSystems_RpgsystemId",
                table: "Progressions");

            migrationBuilder.DropForeignKey(
                name: "FK_RPGElementDefinitions_RPGSystems_RPGSystemId",
                table: "RPGElementDefinitions");

            migrationBuilder.DropForeignKey(
                name: "FK_VariantDefinitions_LevelableDefinitions_LevelableDefinitionId",
                table: "VariantDefinitions");

            migrationBuilder.DropColumn(
                name: "ElementDefinitionId",
                table: "VariantDefinitions");

            migrationBuilder.RenameColumn(
                name: "RpgsystemId",
                table: "Progressions",
                newName: "RPGSystemId");

            migrationBuilder.RenameIndex(
                name: "IX_Progressions_RpgsystemId",
                table: "Progressions",
                newName: "IX_Progressions_RPGSystemId");

            migrationBuilder.AlterColumn<int>(
                name: "LevelableDefinitionId",
                table: "VariantDefinitions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDefault",
                table: "VariantDefinitions",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "RPGSystemId",
                table: "RPGElementDefinitions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Human",
                table: "RPGElementDefinitions",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "RPGSystemId",
                table: "Progressions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Linear",
                table: "Progressions",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "CustomProgression",
                table: "Progressions",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "EnforceMaxLevel",
                table: "LevelableDefinitions",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddForeignKey(
                name: "FK_Progressions_RPGSystems_RPGSystemId",
                table: "Progressions",
                column: "RPGSystemId",
                principalTable: "RPGSystems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RPGElementDefinitions_RPGSystems_RPGSystemId",
                table: "RPGElementDefinitions",
                column: "RPGSystemId",
                principalTable: "RPGSystems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VariantDefinitions_LevelableDefinitions_LevelableDefinitionId",
                table: "VariantDefinitions",
                column: "LevelableDefinitionId",
                principalTable: "LevelableDefinitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Progressions_RPGSystems_RPGSystemId",
                table: "Progressions");

            migrationBuilder.DropForeignKey(
                name: "FK_RPGElementDefinitions_RPGSystems_RPGSystemId",
                table: "RPGElementDefinitions");

            migrationBuilder.DropForeignKey(
                name: "FK_VariantDefinitions_LevelableDefinitions_LevelableDefinitionId",
                table: "VariantDefinitions");

            migrationBuilder.RenameColumn(
                name: "RPGSystemId",
                table: "Progressions",
                newName: "RpgsystemId");

            migrationBuilder.RenameIndex(
                name: "IX_Progressions_RPGSystemId",
                table: "Progressions",
                newName: "IX_Progressions_RpgsystemId");

            migrationBuilder.AlterColumn<int>(
                name: "LevelableDefinitionId",
                table: "VariantDefinitions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDefault",
                table: "VariantDefinitions",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ElementDefinitionId",
                table: "VariantDefinitions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "RPGSystemId",
                table: "RPGElementDefinitions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "Human",
                table: "RPGElementDefinitions",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<int>(
                name: "RpgsystemId",
                table: "Progressions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "Linear",
                table: "Progressions",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "CustomProgression",
                table: "Progressions",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "EnforceMaxLevel",
                table: "LevelableDefinitions",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Progressions_RPGSystems_RpgsystemId",
                table: "Progressions",
                column: "RpgsystemId",
                principalTable: "RPGSystems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RPGElementDefinitions_RPGSystems_RPGSystemId",
                table: "RPGElementDefinitions",
                column: "RPGSystemId",
                principalTable: "RPGSystems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VariantDefinitions_LevelableDefinitions_LevelableDefinitionId",
                table: "VariantDefinitions",
                column: "LevelableDefinitionId",
                principalTable: "LevelableDefinitions",
                principalColumn: "Id");
        }
    }
}
