using System.ComponentModel.DataAnnotations;

namespace Triarch.Database.Models.Ruleset;

public class RPGSystem
{
    [Key]
    public int Id { get; set; }

    public CoreRuleset Ruleset { get; set; } = null!;

    [MaxLength(60)]
    public string SystemName { get; set; } = null!;

    [MaxLength(250)]
    public string? DescriptiveName { get; set; } = null;

    public int OwnerUserId { get; set; } = 1;

    public ICollection<RPGElementType> ElementTypes { get; set; } = null!;

    public ICollection<RPGElementDefinition> ElementDefinitions { get; set; } = null!;

    public ICollection<Genre> Genres { get; set; } = null!;

    public ICollection<Progression> Progressions { get; set; } = null!;
}
