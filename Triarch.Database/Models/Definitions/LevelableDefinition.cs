namespace Triarch.Database.Models.Definitions;

public partial class LevelableDefinition
{
    public int Id { get; set; }

    public int? MaxLevel { get; set; }

    public bool? EnforceMaxLevel { get; set; }

    public int? CostPerLevel { get; set; }

    public string? CostPerLevelDescription { get; set; }

    public int? SpecialPointsPerLevel { get; set; }

    public int? ProgressionId { get; set; }

    public bool? ProgressionReversed { get; set; }

    public virtual ICollection<GenreCostPerLevel> GenreCostPerLevels { get; set; } = new List<GenreCostPerLevel>();

    public virtual Progression? Progression { get; set; }

    public virtual RPGElementDefinition RPGElementDefinition { get; set; } = null!;

    public virtual ICollection<VariantDefinition> VariantDefinitions { get; set; } = new List<VariantDefinition>();
}
