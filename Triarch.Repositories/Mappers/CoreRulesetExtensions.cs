using Triarch.Database.Models.Definitions;
using Triarch.Dtos.Definitions;

namespace Triarch.Repositories.Mappers;

internal static class CoreRulesetExtensions
{
    public static CoreRulesetDto ToDto(this CoreRuleset coreRuleset)
    {
        return new CoreRulesetDto
        {
            Id = coreRuleset.Id,
            CoreRulesetName = coreRuleset.CoreRulesetName
        };
    }
    public static CoreRuleset ToModel(this CoreRulesetDto coreRulesetDto)
    {
        return new CoreRuleset
        {
            Id = coreRulesetDto.Id,
            CoreRulesetName = coreRulesetDto.CoreRulesetName
        };
    }
}
