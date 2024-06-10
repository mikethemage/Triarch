using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triarch.Database.Models.Ruleset;
using Triarch.Dtos.Ruleset;

namespace Triarch.Repositories.Mappers;
internal static class GenreExtensions
{
    public static GenreDto ToDto(this Genre genre)
    {
        return new GenreDto
        {
            Id = genre.Id,
            GenreName = genre.GenreName,
            GenreOrder = genre.GenreOrder
        };
    }

    public static Genre ToModel(this GenreDto genreDto)
    {
        return new Genre
        {
            Id = genreDto.Id,
            GenreName = genreDto.GenreName,
            GenreOrder = genreDto.GenreOrder
        };
    }
}
