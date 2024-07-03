using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TriarchDbFirst.Migrations
{
    /// <inheritdoc />
    public partial class RedefineJoinTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RPGElementDefinitionRPGElementDefinition");

            migrationBuilder.CreateTable(
                name: "ElementRelations",
                columns: table => new
                {
                    ChildId = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElementRelations", x => new { x.ChildId, x.ParentId });
                    table.ForeignKey(
                        name: "FK_ElementRelations_RPGElementDefinitions_ChildId",
                        column: x => x.ChildId,
                        principalTable: "RPGElementDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElementRelations_RPGElementDefinitions_ParentId",
                        column: x => x.ParentId,
                        principalTable: "RPGElementDefinitions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ElementRelations_ParentId",
                table: "ElementRelations",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElementRelations");

            migrationBuilder.CreateTable(
                name: "RPGElementDefinitionRPGElementDefinition",
                columns: table => new
                {
                    AllowedChildrenId = table.Column<int>(type: "int", nullable: false),
                    RPGElementDefinitionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RPGElementDefinitionRPGElementDefinition", x => new { x.AllowedChildrenId, x.RPGElementDefinitionId });
                    table.ForeignKey(
                        name: "FK_RPGElementDefinitionRPGElementDefinition_RPGElementDefinitions_AllowedChildrenId",
                        column: x => x.AllowedChildrenId,
                        principalTable: "RPGElementDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RPGElementDefinitionRPGElementDefinition_RPGElementDefinitions_RPGElementDefinitionId",
                        column: x => x.RPGElementDefinitionId,
                        principalTable: "RPGElementDefinitions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RPGElementDefinitionRPGElementDefinition_RPGElementDefinitionId",
                table: "RPGElementDefinitionRPGElementDefinition",
                column: "RPGElementDefinitionId");
        }
    }
}
