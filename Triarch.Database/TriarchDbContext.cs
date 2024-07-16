using Microsoft.EntityFrameworkCore;
using Triarch.Database.Models.Definitions;

namespace Triarch.Database;

public class TriarchDbContext : DbContext
{

    public TriarchDbContext() : base()
    {

    }

    public TriarchDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
           "Server=.;Database=Triarch;Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True");
    }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<GenreCostPerLevel> GenreCostPerLevels { get; set; }

    public virtual DbSet<Progression> Progressions { get; set; }

    public virtual DbSet<ProgressionEntry> ProgressionEntries { get; set; }

    public virtual DbSet<RPGElementType> RPGElementTypes { get; set; }

    public virtual DbSet<RPGFreebie> RPGFreebies { get; set; }

    public virtual DbSet<VariantDefinition> VariantDefinitions { get; set; }

    public virtual DbSet<LevelableDefinition> LevelableDefinitions { get; set; }

    public virtual DbSet<RPGElementDefinition> RPGElementDefinitions { get; set; }

    public virtual DbSet<CoreRuleset> CoreRulesets { get; set; }

    public virtual DbSet<RPGSystem> RPGSystems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CoreRuleset>(entity =>
        {
            entity.Property(e => e.CoreRulesetName).HasMaxLength(60);
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.Property(e => e.GenreName).HasMaxLength(100);

            entity.HasOne(d => d.RPGSystem).WithMany(p => p.Genres).HasForeignKey(d => d.RPGSystemId);
        });

        modelBuilder.Entity<GenreCostPerLevel>(entity =>
        {
            entity.HasOne(d => d.Genre).WithMany(p => p.GenreCostPerLevels).HasForeignKey(d => d.GenreId);

            entity.HasOne(d => d.Levelable).WithMany(p => p.GenreCostPerLevels).HasForeignKey(d => d.LevelableId);
        });

        modelBuilder.Entity<LevelableDefinition>(entity =>
        {
            entity.Property(e => e.CostPerLevelDescription).HasMaxLength(100);
            entity.Property(e => e.EnforceMaxLevel).HasDefaultValue(false);


            entity.HasOne(d => d.Progression).WithMany(p => p.LevelableDefinitions).HasForeignKey(d => d.ProgressionId);
        });

        modelBuilder.Entity<Progression>(entity =>
        {
            entity.Property(e => e.ProgressionType).HasMaxLength(100);
            entity.Property(e => e.CustomProgression).HasDefaultValue(false);
            entity.Property(e => e.Linear).HasDefaultValue(false);

            entity.HasOne(d => d.RPGSystem).WithMany(p => p.Progressions).HasForeignKey(d => d.RPGSystemId);
        });

        modelBuilder.Entity<ProgressionEntry>(entity =>
        {
            entity.Property(e => e.Text).HasMaxLength(400);

            entity.HasOne(d => d.Progression).WithMany(p => p.ProgressionEntries).HasForeignKey(d => d.ProgressionId);
        });

        modelBuilder.Entity<RPGElementDefinition>(entity =>
        {
            entity.Property(e => e.Description).HasMaxLength(600);
            entity.Property(e => e.ElementName).HasMaxLength(100);
            entity.Property(e => e.PageNumbers).HasMaxLength(25);
            entity.Property(e => e.Stat).HasMaxLength(50);
            entity.Property(e => e.Human);

            entity.HasOne(d => d.LevelableData)
                  .WithOne(p => p.RPGElementDefinition)
                  .HasForeignKey<RPGElementDefinition>(d => d.LevelableDataId)
                  .IsRequired(false)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.RPGSystem).WithMany(p => p.RPGElementDefinitions).HasForeignKey(d => d.RPGSystemId).OnDelete(DeleteBehavior.NoAction);

            entity.HasMany(d => d.AllowedChildren).WithMany().UsingEntity<Dictionary<string, object>>(
                "ElementRelations",
                j => j.HasOne<RPGElementDefinition>().WithMany().HasForeignKey("ChildId"),
                j => j.HasOne<RPGElementDefinition>().WithMany().HasForeignKey("ParentId")
            );

            entity.HasMany(d => d.Freebies)
                  .WithOne(p => p.OwnerElementDefinition)
                  .HasForeignKey(d => d.OwnerElementDefinitionId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<RPGElementType>(entity =>
        {
            entity.Property(e => e.TypeName).HasMaxLength(100);

            entity.HasOne(d => d.RPGSystem).WithMany(p => p.RPGElementTypes).HasForeignKey(d => d.RPGSystemId);
        });


        modelBuilder.Entity<RPGFreebie>(entity =>
        {
            entity.HasOne(d => d.FreebieElementDefinition).WithMany()
                .HasForeignKey(d => d.FreebieElementDefinitionId).OnDelete(DeleteBehavior.NoAction);


        });

        modelBuilder.Entity<RPGSystem>(entity =>
        {
            entity.Property(e => e.DescriptiveName).HasMaxLength(250);
            entity.Property(e => e.SystemName).HasMaxLength(60);

            entity.HasOne(d => d.Ruleset).WithMany(p => p.RPGSystems).HasForeignKey(d => d.RulesetId);
        });

        modelBuilder.Entity<VariantDefinition>(entity =>
        {
            entity.Property(e => e.Description).HasMaxLength(250);
            entity.Property(e => e.VariantName).HasMaxLength(100);
            entity.Property(e => e.IsDefault).HasDefaultValue(false);

            entity.HasOne(d => d.LevelableDefinition).WithMany(p => p.VariantDefinitions).HasForeignKey(d => d.LevelableDefinitionId).OnDelete(DeleteBehavior.Cascade);
        });

    }
}
