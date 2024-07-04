﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Triarch.Database;

#nullable disable

namespace Triarch.Database.Migrations
{
    [DbContext(typeof(TriarchDbContext))]
    [Migration("20240704142942_Change table structure aaa")]
    partial class Changetablestructureaaa
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ElementRelations", b =>
                {
                    b.Property<int>("ChildId")
                        .HasColumnType("int");

                    b.Property<int>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("ChildId", "ParentId");

                    b.HasIndex("ParentId");

                    b.ToTable("ElementRelations");
                });

            modelBuilder.Entity("Triarch.Database.Models.Definitions.CoreRuleset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CoreRulesetName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.ToTable("CoreRulesets");
                });

            modelBuilder.Entity("Triarch.Database.Models.Definitions.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("GenreName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("GenreOrder")
                        .HasColumnType("int");

                    b.Property<int>("RPGSystemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RPGSystemId");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("Triarch.Database.Models.Definitions.GenreCostPerLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CostPerLevel")
                        .HasColumnType("int");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.Property<int>("LevelableId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.HasIndex("LevelableId");

                    b.ToTable("GenreCostPerLevels");
                });

            modelBuilder.Entity("Triarch.Database.Models.Definitions.LevelableDefinition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CostPerLevel")
                        .HasColumnType("int");

                    b.Property<string>("CostPerLevelDescription")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("EnforceMaxLevel")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int?>("MaxLevel")
                        .HasColumnType("int");

                    b.Property<int?>("ProgressionId")
                        .HasColumnType("int");

                    b.Property<bool?>("ProgressionReversed")
                        .HasColumnType("bit");

                    b.Property<int?>("SpecialPointsPerLevel")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProgressionId");

                    b.ToTable("LevelableDefinitions");
                });

            modelBuilder.Entity("Triarch.Database.Models.Definitions.Progression", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("CustomProgression")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("Linear")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("ProgressionType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("RPGSystemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RPGSystemId");

                    b.ToTable("Progressions");
                });

            modelBuilder.Entity("Triarch.Database.Models.Definitions.ProgressionEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ProgressionId")
                        .HasColumnType("int");

                    b.Property<int>("ProgressionLevel")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.HasKey("Id");

                    b.HasIndex("ProgressionId");

                    b.ToTable("ProgressionEntries");
                });

            modelBuilder.Entity("Triarch.Database.Models.Definitions.RPGElementDefinition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(600)
                        .HasColumnType("nvarchar(600)");

                    b.Property<string>("ElementName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("ElementTypeId")
                        .HasColumnType("int");

                    b.Property<bool>("Human")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<int?>("LevelableDataId")
                        .HasColumnType("int");

                    b.Property<string>("PageNumbers")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<int?>("PointsContainerScale")
                        .HasColumnType("int");

                    b.Property<int>("RPGSystemId")
                        .HasColumnType("int");

                    b.Property<string>("Stat")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("LevelableDataId")
                        .IsUnique()
                        .HasFilter("[LevelableDataId] IS NOT NULL");

                    b.HasIndex("RPGSystemId");

                    b.ToTable("RPGElementDefinitions");
                });

            modelBuilder.Entity("Triarch.Database.Models.Definitions.RPGElementType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("RPGSystemId")
                        .HasColumnType("int");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("TypeOrder")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RPGSystemId");

                    b.ToTable("RPGElementTypes");
                });

            modelBuilder.Entity("Triarch.Database.Models.Definitions.RPGFreebie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FreeLevels")
                        .HasColumnType("int");

                    b.Property<int>("FreebieElementDefinitionId")
                        .HasColumnType("int");

                    b.Property<int>("OwnerElementDefinitionId")
                        .HasColumnType("int");

                    b.Property<int>("RequiredLevels")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FreebieElementDefinitionId");

                    b.HasIndex("OwnerElementDefinitionId");

                    b.ToTable("RPGFreebies");
                });

            modelBuilder.Entity("Triarch.Database.Models.Definitions.RPGSystem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DescriptiveName")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("OwnerUserId")
                        .HasColumnType("int");

                    b.Property<int>("RulesetId")
                        .HasColumnType("int");

                    b.Property<string>("SystemName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.HasIndex("RulesetId");

                    b.ToTable("RPGSystems");
                });

            modelBuilder.Entity("Triarch.Database.Models.Definitions.VariantDefinition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CostPerLevel")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<bool>("IsDefault")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("LevelableDefinitionId")
                        .HasColumnType("int");

                    b.Property<string>("VariantName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("LevelableDefinitionId");

                    b.ToTable("VariantDefinitions");
                });

            modelBuilder.Entity("ElementRelations", b =>
                {
                    b.HasOne("Triarch.Database.Models.Definitions.RPGElementDefinition", null)
                        .WithMany()
                        .HasForeignKey("ChildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Triarch.Database.Models.Definitions.RPGElementDefinition", null)
                        .WithMany()
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Triarch.Database.Models.Definitions.Genre", b =>
                {
                    b.HasOne("Triarch.Database.Models.Definitions.RPGSystem", "RPGSystem")
                        .WithMany("Genres")
                        .HasForeignKey("RPGSystemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RPGSystem");
                });

            modelBuilder.Entity("Triarch.Database.Models.Definitions.GenreCostPerLevel", b =>
                {
                    b.HasOne("Triarch.Database.Models.Definitions.Genre", "Genre")
                        .WithMany("GenreCostPerLevels")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Triarch.Database.Models.Definitions.LevelableDefinition", "Levelable")
                        .WithMany("GenreCostPerLevels")
                        .HasForeignKey("LevelableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("Levelable");
                });

            modelBuilder.Entity("Triarch.Database.Models.Definitions.LevelableDefinition", b =>
                {
                    b.HasOne("Triarch.Database.Models.Definitions.Progression", "Progression")
                        .WithMany("LevelableDefinitions")
                        .HasForeignKey("ProgressionId");

                    b.Navigation("Progression");
                });

            modelBuilder.Entity("Triarch.Database.Models.Definitions.Progression", b =>
                {
                    b.HasOne("Triarch.Database.Models.Definitions.RPGSystem", "RPGSystem")
                        .WithMany("Progressions")
                        .HasForeignKey("RPGSystemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RPGSystem");
                });

            modelBuilder.Entity("Triarch.Database.Models.Definitions.ProgressionEntry", b =>
                {
                    b.HasOne("Triarch.Database.Models.Definitions.Progression", "Progression")
                        .WithMany("ProgressionEntries")
                        .HasForeignKey("ProgressionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Progression");
                });

            modelBuilder.Entity("Triarch.Database.Models.Definitions.RPGElementDefinition", b =>
                {
                    b.HasOne("Triarch.Database.Models.Definitions.LevelableDefinition", "LevelableData")
                        .WithOne("RPGElementDefinition")
                        .HasForeignKey("Triarch.Database.Models.Definitions.RPGElementDefinition", "LevelableDataId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Triarch.Database.Models.Definitions.RPGSystem", "RPGSystem")
                        .WithMany("RPGElementDefinitions")
                        .HasForeignKey("RPGSystemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LevelableData");

                    b.Navigation("RPGSystem");
                });

            modelBuilder.Entity("Triarch.Database.Models.Definitions.RPGElementType", b =>
                {
                    b.HasOne("Triarch.Database.Models.Definitions.RPGSystem", "RPGSystem")
                        .WithMany("RPGElementTypes")
                        .HasForeignKey("RPGSystemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RPGSystem");
                });

            modelBuilder.Entity("Triarch.Database.Models.Definitions.RPGFreebie", b =>
                {
                    b.HasOne("Triarch.Database.Models.Definitions.RPGElementDefinition", "FreebieElementDefinition")
                        .WithMany()
                        .HasForeignKey("FreebieElementDefinitionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Triarch.Database.Models.Definitions.RPGElementDefinition", "OwnerElementDefinition")
                        .WithMany("Freebies")
                        .HasForeignKey("OwnerElementDefinitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FreebieElementDefinition");

                    b.Navigation("OwnerElementDefinition");
                });

            modelBuilder.Entity("Triarch.Database.Models.Definitions.RPGSystem", b =>
                {
                    b.HasOne("Triarch.Database.Models.Definitions.CoreRuleset", "Ruleset")
                        .WithMany("RPGSystems")
                        .HasForeignKey("RulesetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ruleset");
                });

            modelBuilder.Entity("Triarch.Database.Models.Definitions.VariantDefinition", b =>
                {
                    b.HasOne("Triarch.Database.Models.Definitions.LevelableDefinition", "LevelableDefinition")
                        .WithMany("VariantDefinitions")
                        .HasForeignKey("LevelableDefinitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LevelableDefinition");
                });

            modelBuilder.Entity("Triarch.Database.Models.Definitions.CoreRuleset", b =>
                {
                    b.Navigation("RPGSystems");
                });

            modelBuilder.Entity("Triarch.Database.Models.Definitions.Genre", b =>
                {
                    b.Navigation("GenreCostPerLevels");
                });

            modelBuilder.Entity("Triarch.Database.Models.Definitions.LevelableDefinition", b =>
                {
                    b.Navigation("GenreCostPerLevels");

                    b.Navigation("RPGElementDefinition")
                        .IsRequired();

                    b.Navigation("VariantDefinitions");
                });

            modelBuilder.Entity("Triarch.Database.Models.Definitions.Progression", b =>
                {
                    b.Navigation("LevelableDefinitions");

                    b.Navigation("ProgressionEntries");
                });

            modelBuilder.Entity("Triarch.Database.Models.Definitions.RPGElementDefinition", b =>
                {
                    b.Navigation("Freebies");
                });

            modelBuilder.Entity("Triarch.Database.Models.Definitions.RPGSystem", b =>
                {
                    b.Navigation("Genres");

                    b.Navigation("Progressions");

                    b.Navigation("RPGElementDefinitions");

                    b.Navigation("RPGElementTypes");
                });
#pragma warning restore 612, 618
        }
    }
}
