using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class RPGSystemDTO
{
    [Key]
    public int Id { get; set; }

    public CoreRulesetDTO Ruleset { get; set; } = null!; 
    
    [MaxLength(60)]
    public string SystemName { get; set; } = null!;  

    [MaxLength(250)]
    public string? DescriptiveName { get; set; } = null;

    public int OwnerUserId { get; set; } = 1;   
    
}
