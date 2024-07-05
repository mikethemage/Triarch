using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Triarch.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoreRulesets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoreRulesetName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoreRulesets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RPGSystems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RulesetId = table.Column<int>(type: "int", nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    DescriptiveName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    OwnerUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RPGSystems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RPGSystems_CoreRulesets_RulesetId",
                        column: x => x.RulesetId,
                        principalTable: "CoreRulesets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GenreOrder = table.Column<int>(type: "int", nullable: false),
                    RPGSystemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Genres_RPGSystems_RPGSystemId",
                        column: x => x.RPGSystemId,
                        principalTable: "RPGSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Progressions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgressionType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CustomProgression = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RPGSystemId = table.Column<int>(type: "int", nullable: false),
                    Linear = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progressions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Progressions_RPGSystems_RPGSystemId",
                        column: x => x.RPGSystemId,
                        principalTable: "RPGSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RPGElementTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TypeOrder = table.Column<int>(type: "int", nullable: false),
                    BuiltIn = table.Column<bool>(type: "bit", nullable: false),
                    RPGSystemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RPGElementTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RPGElementTypes_RPGSystems_RPGSystemId",
                        column: x => x.RPGSystemId,
                        principalTable: "RPGSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LevelableDefinitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaxLevel = table.Column<int>(type: "int", nullable: true),
                    EnforceMaxLevel = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    CostPerLevel = table.Column<int>(type: "int", nullable: true),
                    CostPerLevelDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SpecialPointsPerLevel = table.Column<int>(type: "int", nullable: true),
                    ProgressionId = table.Column<int>(type: "int", nullable: true),
                    ProgressionReversed = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelableDefinitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LevelableDefinitions_Progressions_ProgressionId",
                        column: x => x.ProgressionId,
                        principalTable: "Progressions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProgressionEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    ProgressionLevel = table.Column<int>(type: "int", nullable: false),
                    ProgressionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressionEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgressionEntries_Progressions_ProgressionId",
                        column: x => x.ProgressionId,
                        principalTable: "Progressions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GenreCostPerLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    LevelableId = table.Column<int>(type: "int", nullable: false),
                    CostPerLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreCostPerLevels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GenreCostPerLevels_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreCostPerLevels_LevelableDefinitions_LevelableId",
                        column: x => x.LevelableId,
                        principalTable: "LevelableDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RPGElementDefinitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElementName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ElementTypeId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: true),
                    Stat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PageNumbers = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Human = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    LevelableDataId = table.Column<int>(type: "int", nullable: true),
                    PointsContainerScale = table.Column<int>(type: "int", nullable: true),
                    RPGSystemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RPGElementDefinitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RPGElementDefinitions_LevelableDefinitions_LevelableDataId",
                        column: x => x.LevelableDataId,
                        principalTable: "LevelableDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_RPGElementDefinitions_RPGElementTypes_ElementTypeId",
                        column: x => x.ElementTypeId,
                        principalTable: "RPGElementTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RPGElementDefinitions_RPGSystems_RPGSystemId",
                        column: x => x.RPGSystemId,
                        principalTable: "RPGSystems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VariantDefinitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VariantName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CostPerLevel = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LevelableDefinitionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariantDefinitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VariantDefinitions_LevelableDefinitions_LevelableDefinitionId",
                        column: x => x.LevelableDefinitionId,
                        principalTable: "LevelableDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "RPGFreebies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FreebieElementDefinitionId = table.Column<int>(type: "int", nullable: false),
                    FreeLevels = table.Column<int>(type: "int", nullable: false),
                    RequiredLevels = table.Column<int>(type: "int", nullable: false),
                    OwnerElementDefinitionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RPGFreebies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RPGFreebies_RPGElementDefinitions_FreebieElementDefinitionId",
                        column: x => x.FreebieElementDefinitionId,
                        principalTable: "RPGElementDefinitions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RPGFreebies_RPGElementDefinitions_OwnerElementDefinitionId",
                        column: x => x.OwnerElementDefinitionId,
                        principalTable: "RPGElementDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ElementRelations_ParentId",
                table: "ElementRelations",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_GenreCostPerLevels_GenreId",
                table: "GenreCostPerLevels",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_GenreCostPerLevels_LevelableId",
                table: "GenreCostPerLevels",
                column: "LevelableId");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_RPGSystemId",
                table: "Genres",
                column: "RPGSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_LevelableDefinitions_ProgressionId",
                table: "LevelableDefinitions",
                column: "ProgressionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressionEntries_ProgressionId",
                table: "ProgressionEntries",
                column: "ProgressionId");

            migrationBuilder.CreateIndex(
                name: "IX_Progressions_RPGSystemId",
                table: "Progressions",
                column: "RPGSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_RPGElementDefinitions_ElementTypeId",
                table: "RPGElementDefinitions",
                column: "ElementTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RPGElementDefinitions_LevelableDataId",
                table: "RPGElementDefinitions",
                column: "LevelableDataId",
                unique: true,
                filter: "[LevelableDataId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RPGElementDefinitions_RPGSystemId",
                table: "RPGElementDefinitions",
                column: "RPGSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_RPGElementTypes_RPGSystemId",
                table: "RPGElementTypes",
                column: "RPGSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_RPGFreebies_FreebieElementDefinitionId",
                table: "RPGFreebies",
                column: "FreebieElementDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_RPGFreebies_OwnerElementDefinitionId",
                table: "RPGFreebies",
                column: "OwnerElementDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_RPGSystems_RulesetId",
                table: "RPGSystems",
                column: "RulesetId");

            migrationBuilder.CreateIndex(
                name: "IX_VariantDefinitions_LevelableDefinitionId",
                table: "VariantDefinitions",
                column: "LevelableDefinitionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElementRelations");

            migrationBuilder.DropTable(
                name: "GenreCostPerLevels");

            migrationBuilder.DropTable(
                name: "ProgressionEntries");

            migrationBuilder.DropTable(
                name: "RPGFreebies");

            migrationBuilder.DropTable(
                name: "VariantDefinitions");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "RPGElementDefinitions");

            migrationBuilder.DropTable(
                name: "LevelableDefinitions");

            migrationBuilder.DropTable(
                name: "RPGElementTypes");

            migrationBuilder.DropTable(
                name: "Progressions");

            migrationBuilder.DropTable(
                name: "RPGSystems");

            migrationBuilder.DropTable(
                name: "CoreRulesets");
        }
    }
}
