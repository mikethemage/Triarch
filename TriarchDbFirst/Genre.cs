namespace TriarchDbFirst;

public partial class Genre
{
    public int Id { get; set; }

    public string GenreName { get; set; } = string.Empty;

    public int GenreOrder { get; set; }

    public int RPGSystemId { get; set; }

    public virtual ICollection<GenreCostPerLevel> GenreCostPerLevels { get; set; } = new List<GenreCostPerLevel>();

    public virtual RPGSystem RPGSystem { get; set; } = null!;
}
