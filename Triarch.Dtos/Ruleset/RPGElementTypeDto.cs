using System.ComponentModel.DataAnnotations;

namespace Triarch.Dtos.Ruleset;

public class RPGElementTypeDto
{
    public int Id { get; set; }

    [MaxLength(100)]
    public string TypeName { get; set; } = null!;

    public int TypeOrder { get; set; }    
}