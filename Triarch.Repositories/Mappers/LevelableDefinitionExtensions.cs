using Triarch.Database.Models.Definitions;
using Triarch.Dtos.Definitions;

namespace Triarch.Repositories.Mappers;
internal static class LevelableDefinitionExtensions
{
    public static LevelableDefinitionDto ToDto(this LevelableDefinition levelableDefinition)
    {
        return new LevelableDefinitionDto
        {
            Id = levelableDefinition.Id,
            MaxLevel = levelableDefinition.MaxLevel,
            EnforceMaxLevel = levelableDefinition.EnforceMaxLevel,
            CostPerLevel = levelableDefinition.CostPerLevel,
            CostPerLevelDescription = levelableDefinition.CostPerLevelDescription,
            MultiGenreCostPerLevels = levelableDefinition.MultiGenreCostPerLevels?.Select(x => x.ToDto()).ToList(),
            ProgressionName = levelableDefinition.Progression?.ProgressionType,
            Variants = levelableDefinition.Variants?.Select(x => x.ToDto()).ToList(),
            SpecialPointsPerLevel = levelableDefinition.SpecialPointsPerLevel
        };
    }

    public static LevelableDefinition ToModel(this LevelableDefinitionDto levelableDefinitionDto)
    {
        return new LevelableDefinition
        {
            Id = levelableDefinitionDto.Id,
            MaxLevel = levelableDefinitionDto.MaxLevel,
            EnforceMaxLevel = levelableDefinitionDto.EnforceMaxLevel,
            CostPerLevel = levelableDefinitionDto.CostPerLevel,
            CostPerLevelDescription = levelableDefinitionDto.CostPerLevelDescription,

            
            
            
            Variants = levelableDefinitionDto.Variants?.Select(x => x.ToModel()).ToList(),
            SpecialPointsPerLevel = levelableDefinitionDto.SpecialPointsPerLevel
        };
    }
    
}
