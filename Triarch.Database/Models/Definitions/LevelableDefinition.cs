using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Triarch.Database.Models.Definitions;

public class LevelableDefinition
{
    [Key]
    public int Id { get; set; }

    public int? MaxLevel { get; set; }

    [DefaultValue(false)]
    public bool EnforceMaxLevel { get; set; } = false;

    public int? CostPerLevel { get; set; } = null;

    [MaxLength(100)]
    public string? CostPerLevelDescription { get; set; } = null;

    public ICollection<GenreCostPerLevel>? MultiGenreCostPerLevels { get; set; } = null;  //Has to be levelable to have custom points per level

    public Progression? Progression { get; set; } = null; //Has to be levelable to have progression



    public ICollection<VariantDefinition>? Variants { get; set; } = null; //Has to be levelable to have variants?  Are all variants levelable???    

    public int? SpecialPointsPerLevel { get; set; } = null; //Must be levelable to have special points per level
}
