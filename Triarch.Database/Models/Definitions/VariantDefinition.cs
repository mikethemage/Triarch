namespace Triarch.Database.Models.Definitions;

public partial class VariantDefinition
{
    public int Id { get; set; }

    public string VariantName { get; set; } = null!;

    public int CostPerLevel { get; set; }

    public string? Description { get; set; }

    public bool IsDefault { get; set; }

    public int LevelableDefinitionId { get; set; }

    public virtual LevelableDefinition? LevelableDefinition { get; set; }
}
