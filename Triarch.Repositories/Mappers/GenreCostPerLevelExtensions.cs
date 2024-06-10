using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triarch.Database.Models.Ruleset;
using Triarch.Dtos.Ruleset;

namespace Triarch.Repositories.Mappers;
internal static class GenreCostPerLevelExtensions
{
    public static GenreCostPerLevelDto ToDto(this GenreCostPerLevel genreCostPerLevel)
    {
        return new GenreCostPerLevelDto
        {
            Id = genreCostPerLevel.Id,
            GenreId = genreCostPerLevel.Genre.Id,
            CostPerLevel = genreCostPerLevel.CostPerLevel            
        };
    }

    public static GenreCostPerLevel ToModel(this GenreCostPerLevelDto genreCostPerLevelDto)
    {
        return new GenreCostPerLevel
        {
            Id = genreCostPerLevelDto.Id,
            
            CostPerLevel = genreCostPerLevelDto.CostPerLevel
        };
    }
}
