using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TriarchDbFirst.Migrations
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
                name: "Rpgsystems",
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
                    table.PrimaryKey("PK_Rpgsystems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rpgsystems_CoreRulesets_RulesetId",
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
                        name: "FK_Genres_Rpgsystems_RPGSystemId",
                        column: x => x.RPGSystemId,
                        principalTable: "Rpgsystems",
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
                    CustomProgression = table.Column<bool>(type: "bit", nullable: false),
                    RpgsystemId = table.Column<int>(type: "int", nullable: true),
                    Linear = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progressions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Progressions_Rpgsystems_RpgsystemId",
                        column: x => x.RpgsystemId,
                        principalTable: "Rpgsystems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RpgelementTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TypeOrder = table.Column<int>(type: "int", nullable: false),
                    RPGSystemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RpgelementTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RpgelementTypes_Rpgsystems_RPGSystemId",
                        column: x => x.RPGSystemId,
                        principalTable: "Rpgsystems",
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
                    EnforceMaxLevel = table.Column<bool>(type: "bit", nullable: false),
                    CostPerLevel = table.Column<int>(type: "int", nullable: false),
                    CostPerLevelDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ProgressionId = table.Column<int>(type: "int", nullable: true),
                    SpecialPointsPerLevel = table.Column<int>(type: "int", nullable: true),
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
                name: "RpgelementDefinitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElementName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ElementTypeId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: true),
                    Stat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PageNumbers = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Human = table.Column<bool>(type: "bit", nullable: false),
                    LevelableDataId = table.Column<int>(type: "int", nullable: true),
                    PointsContainerScale = table.Column<int>(type: "int", nullable: true),
                    RPGSystemId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RpgelementDefinitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RpgelementDefinitions_LevelableDefinitions_LevelableDataId",
                        column: x => x.LevelableDataId,
                        principalTable: "LevelableDefinitions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RpgelementDefinitions_Rpgsystems_RPGSystemId",
                        column: x => x.RPGSystemId,
                        principalTable: "Rpgsystems",
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
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    ElementDefinitionId = table.Column<int>(type: "int", nullable: false),
                    LevelableDefinitionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariantDefinitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VariantDefinitions_LevelableDefinitions_LevelableDefinitionId",
                        column: x => x.LevelableDefinitionId,
                        principalTable: "LevelableDefinitions",
                        principalColumn: "Id");
                });

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
                        name: "FK_RPGElementDefinitionRPGElementDefinition_RpgelementDefinitions_AllowedChildrenId",
                        column: x => x.AllowedChildrenId,
                        principalTable: "RpgelementDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RPGElementDefinitionRPGElementDefinition_RpgelementDefinitions_RPGElementDefinitionId",
                        column: x => x.RPGElementDefinitionId,
                        principalTable: "RpgelementDefinitions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Rpgfreebies",
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
                    table.PrimaryKey("PK_Rpgfreebies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rpgfreebies_RpgelementDefinitions_FreebieElementDefinitionId",
                        column: x => x.FreebieElementDefinitionId,
                        principalTable: "RpgelementDefinitions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rpgfreebies_RpgelementDefinitions_OwnerElementDefinitionId",
                        column: x => x.OwnerElementDefinitionId,
                        principalTable: "RpgelementDefinitions",
                        principalColumn: "Id");
                });

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
                name: "IX_Progressions_RpgsystemId",
                table: "Progressions",
                column: "RpgsystemId");

            migrationBuilder.CreateIndex(
                name: "IX_RPGElementDefinitionRPGElementDefinition_RPGElementDefinitionId",
                table: "RPGElementDefinitionRPGElementDefinition",
                column: "RPGElementDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_RpgelementDefinitions_LevelableDataId",
                table: "RpgelementDefinitions",
                column: "LevelableDataId",
                unique: true,
                filter: "[LevelableDataId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RpgelementDefinitions_RPGSystemId",
                table: "RpgelementDefinitions",
                column: "RPGSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_RpgelementTypes_RPGSystemId",
                table: "RpgelementTypes",
                column: "RPGSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_Rpgfreebies_FreebieElementDefinitionId",
                table: "Rpgfreebies",
                column: "FreebieElementDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Rpgfreebies_OwnerElementDefinitionId",
                table: "Rpgfreebies",
                column: "OwnerElementDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Rpgsystems_RulesetId",
                table: "Rpgsystems",
                column: "RulesetId");

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenreCostPerLevels");

            migrationBuilder.DropTable(
                name: "ProgressionEntries");

            migrationBuilder.DropTable(
                name: "RPGElementDefinitionRPGElementDefinition");

            migrationBuilder.DropTable(
                name: "RpgelementTypes");

            migrationBuilder.DropTable(
                name: "Rpgfreebies");

            migrationBuilder.DropTable(
                name: "VariantDefinitions");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "RpgelementDefinitions");

            migrationBuilder.DropTable(
                name: "LevelableDefinitions");

            migrationBuilder.DropTable(
                name: "Progressions");

            migrationBuilder.DropTable(
                name: "Rpgsystems");

            migrationBuilder.DropTable(
                name: "CoreRulesets");
        }
    }
}
