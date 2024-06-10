using System.ComponentModel.DataAnnotations;

namespace Triarch.Dtos.Ruleset;

public class GenreDto
{
    public int Id { get; set; }

    [MaxLength(100)]
    public string GenreName { get; set; } = string.Empty;

    public int GenreOrder { get; set; }
}