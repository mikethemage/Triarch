namespace Triarch.Database.Models.Definitions;

public partial class RPGFreebie
{
    public int Id { get; set; }

    public int FreebieElementDefinitionId { get; set; }

    public int FreeLevels { get; set; }

    public int RequiredLevels { get; set; }

    public int OwnerElementDefinitionId { get; set; }

    public virtual RPGElementDefinition FreebieElementDefinition { get; set; } = null!;

    public virtual RPGElementDefinition OwnerElementDefinition { get; set; } = null!;
}
