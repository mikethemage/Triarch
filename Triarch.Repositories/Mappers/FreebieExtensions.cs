using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triarch.Database.Models.Ruleset;
using Triarch.Dtos.Ruleset;

namespace Triarch.Repositories.Mappers;
internal static class FreebieExtensions
{
    public static FreebieDto ToDto(this Freebie freebie)
    {
        return new FreebieDto
        {
            Id = freebie.Id,
            FreebieElementDefinitionId = freebie.FreebieElementDefinitionId,
            FreeLevels = freebie.FreeLevels,
            RequiredLevels = freebie.RequiredLevels            
        };
    }
    
    public static Freebie ToModel(this FreebieDto freebieDto)
    {
        return new Freebie
        {
            Id = freebieDto.Id,
            FreebieElementDefinitionId = freebieDto.FreebieElementDefinitionId,
            FreeLevels = freebieDto.FreeLevels,
            RequiredLevels = freebieDto.RequiredLevels
        };
    }
}
