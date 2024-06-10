using Triarch.Database.Models.Definitions;
using Triarch.Dtos.Definitions;

namespace Triarch.Repositories.Mappers;

internal static class RPGSystemExtensions
{
    public static RPGSystemDto ToDto(this RPGSystem rpgSystem)
    {
        return new RPGSystemDto
        {
            Id = rpgSystem.Id,
            Ruleset = rpgSystem.Ruleset.ToDto(),
            SystemName = rpgSystem.SystemName,
            DescriptiveName = rpgSystem.DescriptiveName,
            OwnerUserId = rpgSystem.OwnerUserId,
            ElementTypes = rpgSystem.ElementTypes.Select(x => x.ToDto()).ToList(),
            ElementDefinitions = rpgSystem.ElementDefinitions.Select(x => x.ToDto()).ToList(),
            Genres = rpgSystem.Genres.Select(x => x.ToDto()).ToList(),
            Progressions = rpgSystem.Progressions.Select(x => x.ToDto()).ToList()
        };
    }

    public static RPGSystemHeadingDto ToHeadingDto(this RPGSystem rpgSystem)
    {
        return new RPGSystemHeadingDto
        {
            Id = rpgSystem.Id,
            SystemName = rpgSystem.SystemName,
            CoreRulesetName = rpgSystem.Ruleset.CoreRulesetName
        };
    }

    public static RPGSystem ToModel(this RPGSystemDto rpgSystemDto)
    {
        RPGSystem model = new RPGSystem
        {
            Id = rpgSystemDto.Id,
            Ruleset = rpgSystemDto.Ruleset.ToModel(),
            SystemName = rpgSystemDto.SystemName,
            DescriptiveName = rpgSystemDto.DescriptiveName,
            OwnerUserId = rpgSystemDto.OwnerUserId,
            ElementTypes = rpgSystemDto.ElementTypes.Select(x => x.ToModel()).ToList(),
            ElementDefinitions = rpgSystemDto.ElementDefinitions.Select(x => x.ToModel()).ToList(),
            Genres = rpgSystemDto.Genres.Select(x => x.ToModel()).ToList(),
            Progressions = rpgSystemDto.Progressions.Select(x => x.ToModel()).ToList()
        };

        foreach (RPGElementDefinition elementDefinition in model.ElementDefinitions)
        {
            int id = elementDefinition.Id;
            List<int> allowedChildrenIds = rpgSystemDto.ElementDefinitions.First(x => x.Id == id).AllowedChildrenIds;
            List<RPGElementDefinition> allowedChildren = model.ElementDefinitions.Where(x => allowedChildrenIds.Contains(x.Id)).ToList();
            elementDefinition.AllowedChildren = allowedChildren;

            List<int> allowedParentsIds = rpgSystemDto.ElementDefinitions.First(x => x.Id == id).AllowedParentsIds;
            List<RPGElementDefinition> allowedParents = model.ElementDefinitions.Where(x => allowedParentsIds.Contains(x.Id)).ToList();
            elementDefinition.AllowedParents = allowedParents;

            if (elementDefinition.LevelableData != null)
            {
                int? progressionId = rpgSystemDto.ElementDefinitions.First(x => x.Id == id).LevelableData?.ProgressionId;
                if (progressionId != null)
                {
                    elementDefinition.LevelableData.Progression = model.Progressions.First(x => x.Id == progressionId);
                }
            }
        }

        return model;
    }
}
