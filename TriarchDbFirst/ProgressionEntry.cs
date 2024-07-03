namespace TriarchDbFirst;

public partial class ProgressionEntry
{
    public int Id { get; set; }

    public string Text { get; set; } = string.Empty;

    public int ProgressionLevel { get; set; }

    public int ProgressionId { get; set; }

    public virtual Progression Progression { get; set; } = null!;
}
