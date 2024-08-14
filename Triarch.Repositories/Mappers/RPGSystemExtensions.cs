using Triarch.Database.Models.Definitions;
using Triarch.Dtos.Definitions;

namespace Triarch.Repositories.Mappers;
internal static class RPGSystemExtensions
{
    public static RPGSystemDto ToDto(this RPGSystem model)
    {
        RPGSystemDto output = new RPGSystemDto
        {
            SystemName = model.SystemName,
            DescriptiveName = model.DescriptiveName,
            OwnerUserId = model.OwnerUserId,
            Ruleset = model.Ruleset.ToDto()
        };

        output.Genres = model.Genres.Select(x =>
                        new GenreDto
                        {
                            GenreName = x.GenreName,
                            GenreOrder = x.GenreOrder,
                        }
                    ).ToList();

        List<ProgressionDto> progressionDtos = new List<ProgressionDto>();

        foreach (Progression progression in model.Progressions)
        {
            progressionDtos.Add(new ProgressionDto
            {
                ProgressionType = progression.ProgressionType,
                CustomProgression = progression.CustomProgression,
                Linear = progression.Linear,
                Progressions = progression.ProgressionEntries.Select(x => new ProgressionEntryDto
                {
                    ProgressionLevel = x.ProgressionLevel,
                    Text = x.Text
                }).ToList()
            });
        }

        output.Progressions = progressionDtos;

        output.ElementTypes = model.RPGElementTypes.Select(x => new RPGElementTypeDto
        {
            TypeName = x.TypeName,
            TypeOrder = x.TypeOrder
        }).ToList();

        List<RPGElementDefinitionDto> elementDefinitionDtos = new List<RPGElementDefinitionDto>();

        foreach (RPGElementDefinition elementDefinition in model.RPGElementDefinitions)
        {
            RPGElementDefinitionDto elementDefinitionDto = new RPGElementDefinitionDto
            {
                ElementName = elementDefinition.ElementName,
                ElementTypeName = model.RPGElementTypes.Where(x => x.Id == elementDefinition.ElementTypeId).First().TypeName,
                Description = elementDefinition.Description,
                Human = elementDefinition.Human,
                PageNumbers = elementDefinition.PageNumbers,
                Stat = elementDefinition.Stat,
                PointsContainerScale = elementDefinition.PointsContainerScale,
                AllowedChildrenNames = new List<string>()
            };

            if (elementDefinition.LevelableData != null)
            {
                elementDefinitionDto.LevelableData = new LevelableDefinitionDto
                {
                    CostPerLevel = elementDefinition.LevelableData.CostPerLevel,
                    CostPerLevelDescription = elementDefinition.LevelableData.CostPerLevelDescription,
                    EnforceMaxLevel = elementDefinition.LevelableData.EnforceMaxLevel,
                    MaxLevel = elementDefinition.LevelableData.MaxLevel,
                    ProgressionReversed = elementDefinition.LevelableData.ProgressionReversed,
                    SpecialPointsPerLevel = elementDefinition.LevelableData.SpecialPointsPerLevel
                };
                if (elementDefinition.LevelableData.ProgressionId != null)
                {
                    elementDefinitionDto.LevelableData.ProgressionName =
                         model.Progressions.Where(x => x.Id == elementDefinition.LevelableData.ProgressionId).First().ProgressionType;
                }
                if (elementDefinition.LevelableData.VariantDefinitions != null && elementDefinition.LevelableData.VariantDefinitions.Count > 0)
                {
                    elementDefinitionDto.LevelableData.Variants = new List<VariantDefinitionDto>();

                    foreach (VariantDefinition variantDefinition in elementDefinition.LevelableData.VariantDefinitions)
                    {
                        elementDefinitionDto.LevelableData.Variants.Add(new VariantDefinitionDto
                        {
                            VariantName = variantDefinition.VariantName,
                            Description = variantDefinition.Description,
                            IsDefault = variantDefinition.IsDefault,
                            CostPerLevel = variantDefinition.CostPerLevel
                        });
                    }
                }
                if (elementDefinition.LevelableData.GenreCostPerLevels != null && elementDefinition.LevelableData.GenreCostPerLevels.Count > 0)
                {
                    elementDefinitionDto.LevelableData.MultiGenreCostPerLevels = new List<GenreCostPerLevelDto>();
                    foreach (GenreCostPerLevel genreCostPerLevel in elementDefinition.LevelableData.GenreCostPerLevels)
                    {
                        elementDefinitionDto.LevelableData.MultiGenreCostPerLevels.Add(new GenreCostPerLevelDto
                        {
                            GenreName = genreCostPerLevel.Genre.GenreName,
                            CostPerLevel = genreCostPerLevel.CostPerLevel
                        });
                    }
                }
            }

            foreach (RPGElementDefinition allowedChild in elementDefinition.AllowedChildren)
            {
                elementDefinitionDto.AllowedChildrenNames.Add(allowedChild.ElementName);
            }


            if (elementDefinition.Freebies.Count > 0)
            {
                elementDefinitionDto.Freebies = new List<FreebieDto>();
                foreach (RPGFreebie freebie in elementDefinition.Freebies)
                {
                    elementDefinitionDto.Freebies.Add(new FreebieDto
                    {
                        FreeLevels = freebie.FreeLevels,
                        RequiredLevels = freebie.RequiredLevels,
                        FreebieElementDefinitionName = model.RPGElementDefinitions.Where(x => x.Id == freebie.FreebieElementDefinitionId).First().ElementName
                    });
                }
            }

            elementDefinitionDtos.Add(elementDefinitionDto);
        }

        output.ElementDefinitions = elementDefinitionDtos;

        return output;
    }
}
