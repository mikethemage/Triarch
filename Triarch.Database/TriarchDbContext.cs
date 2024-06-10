using Microsoft.EntityFrameworkCore;
using Triarch.Database.Models.Ruleset;

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

    public DbSet<CoreRuleset> CoreRulesets { get; set; }
    public DbSet<RPGSystem> RPGSystems { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<GenreCostPerLevel> GenreCostPerLevels { get; set; }
    public DbSet<Progression> Progressions { get; set; }
    public DbSet<ProgressionEntry> ProgressionEntries { get; set; }
    public DbSet<RPGElementType> RPGElementTypes { get; set; }
    public DbSet<RPGElementDefinition> RPGElementDefinitions { get; set; }
    public DbSet<LevelableDefinition> LevelableDefinitions { get; set; }
    public DbSet<Freebie> RPGFreebies { get; set; }
    public DbSet<VariantDefinition> VariantDefinitions { get; set; }
}
