using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Triarch.Dtos.Definitions;

public class VariantDefinitionDto
{
    [Required]
    [MaxLength(100)]
    public string VariantName { get; set; } = null!;

    public int CostPerLevel { get; set; }

    [MaxLength(250)]
    public string? Description { get; set; } = null;

    [DefaultValue(false)]
    public bool IsDefault { get; set; } = false;
}