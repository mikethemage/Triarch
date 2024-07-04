namespace Triarch.Database.Models.Definitions;

public partial class RPGSystem
{
    public int Id { get; set; }

    public int RulesetId { get; set; }

    public string SystemName { get; set; } = null!;

    public string? DescriptiveName { get; set; }

    public int OwnerUserId { get; set; }

    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();

    public virtual ICollection<Progression> Progressions { get; set; } = new List<Progression>();

    public virtual ICollection<RPGElementDefinition> RPGElementDefinitions { get; set; } = new List<RPGElementDefinition>();

    public virtual ICollection<RPGElementType> RPGElementTypes { get; set; } = new List<RPGElementType>();

    public virtual CoreRuleset Ruleset { get; set; } = null!;
}
