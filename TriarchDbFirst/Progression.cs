namespace TriarchDbFirst;

public partial class Progression
{
    public int Id { get; set; }

    public string ProgressionType { get; set; } = null!;

    public bool CustomProgression { get; set; }

    public int RPGSystemId { get; set; }

    public bool Linear { get; set; }

    public virtual ICollection<LevelableDefinition> LevelableDefinitions { get; set; } = new List<LevelableDefinition>();

    public virtual ICollection<ProgressionEntry> ProgressionEntries { get; set; } = new List<ProgressionEntry>();

    public virtual RPGSystem RPGSystem { get; set; } = null!;
}
