namespace Triarch.RPGSystem.Models;

public class GenreCostPerLevel
{
    public int Id { get; set; }    
    
    public Genre Genre { get; set; } = null!;    
    
    public LevelableDefinition Levelable { get; set; } = null!;

    public int CostPerLevel { get; set; }
}
