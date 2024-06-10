using System.ComponentModel.DataAnnotations;

namespace Triarch.Database.Models.Definitions;

public class Genre
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string GenreName { get; set; } = null!;

    public int GenreOrder { get; set; }

    public RPGSystem RPGSystem { get; set; } = null!;
}
