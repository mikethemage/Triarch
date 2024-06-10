using System.ComponentModel.DataAnnotations;

namespace Triarch.Dtos.Ruleset;

public class CoreRulesetDto
{
    [Required]
    public int Id { get; set; }

    [MaxLength(60)]
    public string CoreRulesetName { get; set; } = null!;    
}
