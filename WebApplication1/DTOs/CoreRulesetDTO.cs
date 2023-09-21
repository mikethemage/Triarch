using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class CoreRulesetDTO
{
    [Required]
    public int Id { get; set; }

    [MaxLength(60)]
    public string CoreRulesetName { get; set; } = null!;    
}
