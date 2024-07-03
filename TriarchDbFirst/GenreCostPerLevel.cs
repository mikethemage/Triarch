namespace TriarchDbFirst;

public partial class GenreCostPerLevel
{
    public int Id { get; set; }

    public int GenreId { get; set; }

    public int LevelableId { get; set; }

    public int CostPerLevel { get; set; }

    public virtual Genre Genre { get; set; } = null!;

    public virtual LevelableDefinition Levelable { get; set; } = null!;
}
