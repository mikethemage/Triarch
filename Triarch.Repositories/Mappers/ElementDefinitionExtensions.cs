﻿using Triarch.Database.Models.Definitions;
using Triarch.Dtos.Definitions;

namespace Triarch.Repositories.Mappers;
internal static class ElementDefinitionExtensions
{
    public static RPGElementDefinitionDto ToDto(this RPGElementDefinition elementDefinition)
    {
        return new RPGElementDefinitionDto
        {
            Id = elementDefinition.Id,
            ElementName = elementDefinition.ElementName,
            ElementTypeName = elementDefinition.ElementType.TypeName,
            Description = elementDefinition.Description,
            Stat = elementDefinition.Stat,
            PageNumbers = elementDefinition.PageNumbers,
            Human = elementDefinition.Human,

            AllowedChildrenNames = elementDefinition.AllowedChildren.Select(x=>x.ElementName).ToList(),
            AllowedParentsNames = elementDefinition.AllowedParents.Select(x => x.ElementName).ToList(),

            LevelableData = elementDefinition.LevelableData?.ToDto(),

            PointsContainerScale = elementDefinition.PointsContainerScale,

            Freebies = (elementDefinition.Freebies != null && elementDefinition.Freebies.Count > 0) ? elementDefinition.Freebies?.Select(x => x.ToDto()).ToList() : null
        };
    }    

    public static RPGElementDefinition ToModel(this RPGElementDefinitionDto elementDefinitionDto)
    {
        return new RPGElementDefinition
        {
            Id = elementDefinitionDto.Id,
            ElementName = elementDefinitionDto.ElementName,
            
            Description = elementDefinitionDto.Description,
            Stat = elementDefinitionDto.Stat,
            PageNumbers = elementDefinitionDto.PageNumbers,
            Human = elementDefinitionDto.Human,
                        

            LevelableData = elementDefinitionDto.LevelableData?.ToModel(),

            PointsContainerScale = elementDefinitionDto.PointsContainerScale,

            
        };
    }
}
