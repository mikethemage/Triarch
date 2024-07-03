﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TriarchDbFirst;

#nullable disable

namespace TriarchDbFirst.Migrations
{
    [DbContext(typeof(TriarchContext))]
    [Migration("20240703134446_Add defaults to bools")]
    partial class Adddefaultstobools
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RPGElementDefinitionRPGElementDefinition", b =>
                {
                    b.Property<int>("AllowedChildrenId")
                        .HasColumnType("int");

                    b.Property<int>("RPGElementDefinitionId")
                        .HasColumnType("int");

                    b.HasKey("AllowedChildrenId", "RPGElementDefinitionId");

                    b.HasIndex("RPGElementDefinitionId");

                    b.ToTable("RPGElementDefinitionRPGElementDefinition");
                });

            modelBuilder.Entity("TriarchDbFirst.CoreRuleset", b =>
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

            modelBuilder.Entity("TriarchDbFirst.Genre", b =>
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

            modelBuilder.Entity("TriarchDbFirst.GenreCostPerLevel", b =>
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

            modelBuilder.Entity("TriarchDbFirst.LevelableDefinition", b =>
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

            modelBuilder.Entity("TriarchDbFirst.Progression", b =>
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

            modelBuilder.Entity("TriarchDbFirst.ProgressionEntry", b =>
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

            modelBuilder.Entity("TriarchDbFirst.RPGElementDefinition", b =>
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

            modelBuilder.Entity("TriarchDbFirst.RPGElementType", b =>
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

            modelBuilder.Entity("TriarchDbFirst.RPGFreebie", b =>
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

            modelBuilder.Entity("TriarchDbFirst.RPGSystem", b =>
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

            modelBuilder.Entity("TriarchDbFirst.VariantDefinition", b =>
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

            modelBuilder.Entity("RPGElementDefinitionRPGElementDefinition", b =>
                {
                    b.HasOne("TriarchDbFirst.RPGElementDefinition", null)
                        .WithMany()
                        .HasForeignKey("AllowedChildrenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TriarchDbFirst.RPGElementDefinition", null)
                        .WithMany()
                        .HasForeignKey("RPGElementDefinitionId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TriarchDbFirst.Genre", b =>
                {
                    b.HasOne("TriarchDbFirst.RPGSystem", "RPGSystem")
                        .WithMany("Genres")
                        .HasForeignKey("RPGSystemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RPGSystem");
                });

            modelBuilder.Entity("TriarchDbFirst.GenreCostPerLevel", b =>
                {
                    b.HasOne("TriarchDbFirst.Genre", "Genre")
                        .WithMany("GenreCostPerLevels")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TriarchDbFirst.LevelableDefinition", "Levelable")
                        .WithMany("GenreCostPerLevels")
                        .HasForeignKey("LevelableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("Levelable");
                });

            modelBuilder.Entity("TriarchDbFirst.LevelableDefinition", b =>
                {
                    b.HasOne("TriarchDbFirst.Progression", "Progression")
                        .WithMany("LevelableDefinitions")
                        .HasForeignKey("ProgressionId");

                    b.Navigation("Progression");
                });

            modelBuilder.Entity("TriarchDbFirst.Progression", b =>
                {
                    b.HasOne("TriarchDbFirst.RPGSystem", "RPGSystem")
                        .WithMany("Progressions")
                        .HasForeignKey("RPGSystemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RPGSystem");
                });

            modelBuilder.Entity("TriarchDbFirst.ProgressionEntry", b =>
                {
                    b.HasOne("TriarchDbFirst.Progression", "Progression")
                        .WithMany("ProgressionEntries")
                        .HasForeignKey("ProgressionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Progression");
                });

            modelBuilder.Entity("TriarchDbFirst.RPGElementDefinition", b =>
                {
                    b.HasOne("TriarchDbFirst.LevelableDefinition", "LevelableData")
                        .WithOne("RPGElementDefinition")
                        .HasForeignKey("TriarchDbFirst.RPGElementDefinition", "LevelableDataId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("TriarchDbFirst.RPGSystem", "RPGSystem")
                        .WithMany("RPGElementDefinitions")
                        .HasForeignKey("RPGSystemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LevelableData");

                    b.Navigation("RPGSystem");
                });

            modelBuilder.Entity("TriarchDbFirst.RPGElementType", b =>
                {
                    b.HasOne("TriarchDbFirst.RPGSystem", "RPGSystem")
                        .WithMany("RPGElementTypes")
                        .HasForeignKey("RPGSystemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RPGSystem");
                });

            modelBuilder.Entity("TriarchDbFirst.RPGFreebie", b =>
                {
                    b.HasOne("TriarchDbFirst.RPGElementDefinition", "FreebieElementDefinition")
                        .WithMany()
                        .HasForeignKey("FreebieElementDefinitionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TriarchDbFirst.RPGElementDefinition", "OwnerElementDefinition")
                        .WithMany("Freebies")
                        .HasForeignKey("OwnerElementDefinitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FreebieElementDefinition");

                    b.Navigation("OwnerElementDefinition");
                });

            modelBuilder.Entity("TriarchDbFirst.RPGSystem", b =>
                {
                    b.HasOne("TriarchDbFirst.CoreRuleset", "Ruleset")
                        .WithMany("RPGSystems")
                        .HasForeignKey("RulesetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ruleset");
                });

            modelBuilder.Entity("TriarchDbFirst.VariantDefinition", b =>
                {
                    b.HasOne("TriarchDbFirst.LevelableDefinition", "LevelableDefinition")
                        .WithMany("VariantDefinitions")
                        .HasForeignKey("LevelableDefinitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LevelableDefinition");
                });

            modelBuilder.Entity("TriarchDbFirst.CoreRuleset", b =>
                {
                    b.Navigation("RPGSystems");
                });

            modelBuilder.Entity("TriarchDbFirst.Genre", b =>
                {
                    b.Navigation("GenreCostPerLevels");
                });

            modelBuilder.Entity("TriarchDbFirst.LevelableDefinition", b =>
                {
                    b.Navigation("GenreCostPerLevels");

                    b.Navigation("RPGElementDefinition")
                        .IsRequired();

                    b.Navigation("VariantDefinitions");
                });

            modelBuilder.Entity("TriarchDbFirst.Progression", b =>
                {
                    b.Navigation("LevelableDefinitions");

                    b.Navigation("ProgressionEntries");
                });

            modelBuilder.Entity("TriarchDbFirst.RPGElementDefinition", b =>
                {
                    b.Navigation("Freebies");
                });

            modelBuilder.Entity("TriarchDbFirst.RPGSystem", b =>
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
