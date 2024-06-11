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
            string name = elementDefinition.ElementName;

            RPGElementType? elementType = model.ElementTypes.Where(x => x.TypeName == rpgSystemDto.ElementDefinitions.First(x => x.ElementName == name).ElementTypeName).FirstOrDefault();

            if (elementType != null)
            {
                elementDefinition.ElementType = elementType;
            }

            List<string> allowedChildrenNames = rpgSystemDto.ElementDefinitions.First(x => x.ElementName == name).AllowedChildrenNames;
            List<RPGElementDefinition> allowedChildren = model.ElementDefinitions.Where(x => allowedChildrenNames.Contains(x.ElementName)).ToList();
            elementDefinition.AllowedChildren = allowedChildren;

            List<string> allowedParentsNames = rpgSystemDto.ElementDefinitions.First(x => x.ElementName == name).AllowedParentsNames;
            List<RPGElementDefinition> allowedParents = model.ElementDefinitions.Where(x => allowedParentsNames.Contains(x.ElementName)).ToList();
            elementDefinition.AllowedParents = allowedParents;

            if (elementDefinition.LevelableData != null)
            {
                string? progressionName = rpgSystemDto.ElementDefinitions.First(x => x.ElementName == name).LevelableData?.ProgressionName;
                if (progressionName != null)
                {
                    elementDefinition.LevelableData.Progression = model.Progressions.First(x => x.ProgressionType == progressionName);
                }

                List<GenreCostPerLevelDto>? multiGenreData = rpgSystemDto.ElementDefinitions.First(x => x.ElementName == name).LevelableData?.MultiGenreCostPerLevels;
                if (multiGenreData != null)
                {
                    elementDefinition.LevelableData.MultiGenreCostPerLevels = new List<GenreCostPerLevel>();
                    foreach (GenreCostPerLevelDto genreCostPerLevelDto in multiGenreData)
                    {
                        GenreCostPerLevel newGenreCostPerLevel = genreCostPerLevelDto.ToModel();
                        newGenreCostPerLevel.Genre = model.Genres.First(x => x.GenreName == genreCostPerLevelDto.GenreName);
                        elementDefinition.LevelableData.MultiGenreCostPerLevels.Add(newGenreCostPerLevel);
                    }
                }
            }

            if(rpgSystemDto.ElementDefinitions.First(x => x.ElementName == name).Freebies != null)
            {
                foreach (FreebieDto freebieDto in rpgSystemDto.ElementDefinitions.First(x => x.ElementName == name).Freebies!)
                {
                    elementDefinition.Freebies = new List<Freebie>();
                    Freebie newFreebie =  freebieDto.ToModel();
                    newFreebie.FreebieElementDefinition = model.ElementDefinitions.First(x => x.ElementName == freebieDto.FreebieElementDefinitionName);                
                }
            }
        }

        return model;
    }
}
