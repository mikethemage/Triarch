using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TriarchDbFirst.Migrations
{
    /// <inheritdoc />
    public partial class RenameTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Rpgsystems_RPGSystemId",
                table: "Genres");

            migrationBuilder.DropForeignKey(
                name: "FK_Progressions_Rpgsystems_RpgsystemId",
                table: "Progressions");

            migrationBuilder.DropForeignKey(
                name: "FK_RPGElementDefinitionRPGElementDefinition_RpgelementDefinitions_AllowedChildrenId",
                table: "RPGElementDefinitionRPGElementDefinition");

            migrationBuilder.DropForeignKey(
                name: "FK_RPGElementDefinitionRPGElementDefinition_RpgelementDefinitions_RPGElementDefinitionId",
                table: "RPGElementDefinitionRPGElementDefinition");

            migrationBuilder.DropForeignKey(
                name: "FK_RpgelementDefinitions_LevelableDefinitions_LevelableDataId",
                table: "RpgelementDefinitions");

            migrationBuilder.DropForeignKey(
                name: "FK_RpgelementDefinitions_Rpgsystems_RPGSystemId",
                table: "RpgelementDefinitions");

            migrationBuilder.DropForeignKey(
                name: "FK_RpgelementTypes_Rpgsystems_RPGSystemId",
                table: "RpgelementTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Rpgfreebies_RpgelementDefinitions_FreebieElementDefinitionId",
                table: "Rpgfreebies");

            migrationBuilder.DropForeignKey(
                name: "FK_Rpgfreebies_RpgelementDefinitions_OwnerElementDefinitionId",
                table: "Rpgfreebies");

            migrationBuilder.DropForeignKey(
                name: "FK_Rpgsystems_CoreRulesets_RulesetId",
                table: "Rpgsystems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rpgsystems",
                table: "Rpgsystems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rpgfreebies",
                table: "Rpgfreebies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RpgelementTypes",
                table: "RpgelementTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RpgelementDefinitions",
                table: "RpgelementDefinitions");

            migrationBuilder.RenameTable(
                name: "Rpgsystems",
                newName: "RPGSystems");

            migrationBuilder.RenameTable(
                name: "Rpgfreebies",
                newName: "RPGFreebies");

            migrationBuilder.RenameTable(
                name: "RpgelementTypes",
                newName: "RPGElementTypes");

            migrationBuilder.RenameTable(
                name: "RpgelementDefinitions",
                newName: "RPGElementDefinitions");

            migrationBuilder.RenameIndex(
                name: "IX_Rpgsystems_RulesetId",
                table: "RPGSystems",
                newName: "IX_RPGSystems_RulesetId");

            migrationBuilder.RenameIndex(
                name: "IX_Rpgfreebies_OwnerElementDefinitionId",
                table: "RPGFreebies",
                newName: "IX_RPGFreebies_OwnerElementDefinitionId");

            migrationBuilder.RenameIndex(
                name: "IX_Rpgfreebies_FreebieElementDefinitionId",
                table: "RPGFreebies",
                newName: "IX_RPGFreebies_FreebieElementDefinitionId");

            migrationBuilder.RenameIndex(
                name: "IX_RpgelementTypes_RPGSystemId",
                table: "RPGElementTypes",
                newName: "IX_RPGElementTypes_RPGSystemId");

            migrationBuilder.RenameIndex(
                name: "IX_RpgelementDefinitions_RPGSystemId",
                table: "RPGElementDefinitions",
                newName: "IX_RPGElementDefinitions_RPGSystemId");

            migrationBuilder.RenameIndex(
                name: "IX_RpgelementDefinitions_LevelableDataId",
                table: "RPGElementDefinitions",
                newName: "IX_RPGElementDefinitions_LevelableDataId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RPGSystems",
                table: "RPGSystems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RPGFreebies",
                table: "RPGFreebies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RPGElementTypes",
                table: "RPGElementTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RPGElementDefinitions",
                table: "RPGElementDefinitions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_RPGSystems_RPGSystemId",
                table: "Genres",
                column: "RPGSystemId",
                principalTable: "RPGSystems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Progressions_RPGSystems_RpgsystemId",
                table: "Progressions",
                column: "RpgsystemId",
                principalTable: "RPGSystems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RPGElementDefinitionRPGElementDefinition_RPGElementDefinitions_AllowedChildrenId",
                table: "RPGElementDefinitionRPGElementDefinition",
                column: "AllowedChildrenId",
                principalTable: "RPGElementDefinitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RPGElementDefinitionRPGElementDefinition_RPGElementDefinitions_RPGElementDefinitionId",
                table: "RPGElementDefinitionRPGElementDefinition",
                column: "RPGElementDefinitionId",
                principalTable: "RPGElementDefinitions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RPGElementDefinitions_LevelableDefinitions_LevelableDataId",
                table: "RPGElementDefinitions",
                column: "LevelableDataId",
                principalTable: "LevelableDefinitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_RPGElementDefinitions_RPGSystems_RPGSystemId",
                table: "RPGElementDefinitions",
                column: "RPGSystemId",
                principalTable: "RPGSystems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RPGElementTypes_RPGSystems_RPGSystemId",
                table: "RPGElementTypes",
                column: "RPGSystemId",
                principalTable: "RPGSystems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RPGFreebies_RPGElementDefinitions_FreebieElementDefinitionId",
                table: "RPGFreebies",
                column: "FreebieElementDefinitionId",
                principalTable: "RPGElementDefinitions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RPGFreebies_RPGElementDefinitions_OwnerElementDefinitionId",
                table: "RPGFreebies",
                column: "OwnerElementDefinitionId",
                principalTable: "RPGElementDefinitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RPGSystems_CoreRulesets_RulesetId",
                table: "RPGSystems",
                column: "RulesetId",
                principalTable: "CoreRulesets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genres_RPGSystems_RPGSystemId",
                table: "Genres");

            migrationBuilder.DropForeignKey(
                name: "FK_Progressions_RPGSystems_RpgsystemId",
                table: "Progressions");

            migrationBuilder.DropForeignKey(
                name: "FK_RPGElementDefinitionRPGElementDefinition_RPGElementDefinitions_AllowedChildrenId",
                table: "RPGElementDefinitionRPGElementDefinition");

            migrationBuilder.DropForeignKey(
                name: "FK_RPGElementDefinitionRPGElementDefinition_RPGElementDefinitions_RPGElementDefinitionId",
                table: "RPGElementDefinitionRPGElementDefinition");

            migrationBuilder.DropForeignKey(
                name: "FK_RPGElementDefinitions_LevelableDefinitions_LevelableDataId",
                table: "RPGElementDefinitions");

            migrationBuilder.DropForeignKey(
                name: "FK_RPGElementDefinitions_RPGSystems_RPGSystemId",
                table: "RPGElementDefinitions");

            migrationBuilder.DropForeignKey(
                name: "FK_RPGElementTypes_RPGSystems_RPGSystemId",
                table: "RPGElementTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_RPGFreebies_RPGElementDefinitions_FreebieElementDefinitionId",
                table: "RPGFreebies");

            migrationBuilder.DropForeignKey(
                name: "FK_RPGFreebies_RPGElementDefinitions_OwnerElementDefinitionId",
                table: "RPGFreebies");

            migrationBuilder.DropForeignKey(
                name: "FK_RPGSystems_CoreRulesets_RulesetId",
                table: "RPGSystems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RPGSystems",
                table: "RPGSystems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RPGFreebies",
                table: "RPGFreebies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RPGElementTypes",
                table: "RPGElementTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RPGElementDefinitions",
                table: "RPGElementDefinitions");

            migrationBuilder.RenameTable(
                name: "RPGSystems",
                newName: "Rpgsystems");

            migrationBuilder.RenameTable(
                name: "RPGFreebies",
                newName: "Rpgfreebies");

            migrationBuilder.RenameTable(
                name: "RPGElementTypes",
                newName: "RpgelementTypes");

            migrationBuilder.RenameTable(
                name: "RPGElementDefinitions",
                newName: "RpgelementDefinitions");

            migrationBuilder.RenameIndex(
                name: "IX_RPGSystems_RulesetId",
                table: "Rpgsystems",
                newName: "IX_Rpgsystems_RulesetId");

            migrationBuilder.RenameIndex(
                name: "IX_RPGFreebies_OwnerElementDefinitionId",
                table: "Rpgfreebies",
                newName: "IX_Rpgfreebies_OwnerElementDefinitionId");

            migrationBuilder.RenameIndex(
                name: "IX_RPGFreebies_FreebieElementDefinitionId",
                table: "Rpgfreebies",
                newName: "IX_Rpgfreebies_FreebieElementDefinitionId");

            migrationBuilder.RenameIndex(
                name: "IX_RPGElementTypes_RPGSystemId",
                table: "RpgelementTypes",
                newName: "IX_RpgelementTypes_RPGSystemId");

            migrationBuilder.RenameIndex(
                name: "IX_RPGElementDefinitions_RPGSystemId",
                table: "RpgelementDefinitions",
                newName: "IX_RpgelementDefinitions_RPGSystemId");

            migrationBuilder.RenameIndex(
                name: "IX_RPGElementDefinitions_LevelableDataId",
                table: "RpgelementDefinitions",
                newName: "IX_RpgelementDefinitions_LevelableDataId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rpgsystems",
                table: "Rpgsystems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rpgfreebies",
                table: "Rpgfreebies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RpgelementTypes",
                table: "RpgelementTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RpgelementDefinitions",
                table: "RpgelementDefinitions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Rpgsystems_RPGSystemId",
                table: "Genres",
                column: "RPGSystemId",
                principalTable: "Rpgsystems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Progressions_Rpgsystems_RpgsystemId",
                table: "Progressions",
                column: "RpgsystemId",
                principalTable: "Rpgsystems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RPGElementDefinitionRPGElementDefinition_RpgelementDefinitions_AllowedChildrenId",
                table: "RPGElementDefinitionRPGElementDefinition",
                column: "AllowedChildrenId",
                principalTable: "RpgelementDefinitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RPGElementDefinitionRPGElementDefinition_RpgelementDefinitions_RPGElementDefinitionId",
                table: "RPGElementDefinitionRPGElementDefinition",
                column: "RPGElementDefinitionId",
                principalTable: "RpgelementDefinitions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RpgelementDefinitions_LevelableDefinitions_LevelableDataId",
                table: "RpgelementDefinitions",
                column: "LevelableDataId",
                principalTable: "LevelableDefinitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_RpgelementDefinitions_Rpgsystems_RPGSystemId",
                table: "RpgelementDefinitions",
                column: "RPGSystemId",
                principalTable: "Rpgsystems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RpgelementTypes_Rpgsystems_RPGSystemId",
                table: "RpgelementTypes",
                column: "RPGSystemId",
                principalTable: "Rpgsystems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rpgfreebies_RpgelementDefinitions_FreebieElementDefinitionId",
                table: "Rpgfreebies",
                column: "FreebieElementDefinitionId",
                principalTable: "RpgelementDefinitions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rpgfreebies_RpgelementDefinitions_OwnerElementDefinitionId",
                table: "Rpgfreebies",
                column: "OwnerElementDefinitionId",
                principalTable: "RpgelementDefinitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rpgsystems_CoreRulesets_RulesetId",
                table: "Rpgsystems",
                column: "RulesetId",
                principalTable: "CoreRulesets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
