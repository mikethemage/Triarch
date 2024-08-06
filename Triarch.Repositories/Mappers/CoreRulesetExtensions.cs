using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triarch.Database.Models.Definitions;
using Triarch.Dtos.Definitions;

namespace Triarch.Repositories.Mappers;
internal static class CoreRulesetExtensions
{
    public static CoreRulesetDto ToDto(this CoreRuleset model)
    {
        return new CoreRulesetDto
        {            
            CoreRulesetName = model.CoreRulesetName
        };
    }

    public static CoreRuleset ToModel(this CoreRulesetDto dto)
    {
        return new CoreRuleset
        {            
            CoreRulesetName = dto.CoreRulesetName
        };
    }
}
