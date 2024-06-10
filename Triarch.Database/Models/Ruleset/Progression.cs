using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Triarch.Database.Models.Ruleset;

public class Progression
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string ProgressionType { get; set; } = null!;

    [DefaultValue(false)]
    public bool CustomProgression { get; set; } = false;

    public ICollection<ProgressionEntry> Progressions { get; set; } = null!;

    public RPGSystem RPGSystem { get; set; } = null!;
}
