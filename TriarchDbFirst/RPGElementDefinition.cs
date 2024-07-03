namespace TriarchDbFirst;

public partial class RPGElementDefinition
{
    public int Id { get; set; }

    public string ElementName { get; set; } = null!;

    public int ElementTypeId { get; set; }

    public string? Description { get; set; }

    public string? Stat { get; set; }

    public string? PageNumbers { get; set; }

    public bool Human { get; set; }

    public int? LevelableDataId { get; set; }

    public int? PointsContainerScale { get; set; }

    public int RPGSystemId { get; set; }

    public virtual LevelableDefinition? LevelableData { get; set; }   

    public virtual ICollection<RPGFreebie> Freebies { get; set; } = new List<RPGFreebie>();

    public virtual RPGSystem RPGSystem { get; set; } = null!;

    public virtual ICollection<RPGElementDefinition> AllowedChildren { get; set; } = new List<RPGElementDefinition>();    
}
