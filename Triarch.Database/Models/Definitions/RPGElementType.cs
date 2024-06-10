using System.ComponentModel.DataAnnotations;

namespace Triarch.Database.Models.Definitions;

public class RPGElementType
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string TypeName { get; set; } = null!;

    public int TypeOrder { get; set; }

    public RPGSystem RPGSystem { get; set; } = null!;
}
