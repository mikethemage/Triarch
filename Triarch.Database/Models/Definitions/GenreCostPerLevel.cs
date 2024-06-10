using System.ComponentModel.DataAnnotations;

namespace Triarch.Database.Models.Definitions;

public class GenreCostPerLevel
{
    [Key]
    public int Id { get; set; }

    public Genre Genre { get; set; } = null!;

    public LevelableDefinition Levelable { get; set; } = null!;

    public int CostPerLevel { get; set; }
}
