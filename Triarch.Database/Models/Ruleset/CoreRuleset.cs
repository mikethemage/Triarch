using System.ComponentModel.DataAnnotations;

namespace Triarch.Database.Models.Ruleset;

public class CoreRuleset
{
    [Key]
    public int Id { get; set; }

    [MaxLength(60)]
    public string CoreRulesetName { get; set; } = null!;

    public ICollection<RPGSystem> RPGSystems { get; set; } = null!;
}
