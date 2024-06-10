using Triarch.Database.Models.Definitions;
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
            ElementTypeId = elementDefinition.ElementType.Id,
            Description = elementDefinition.Description,
            Stat = elementDefinition.Stat,
            PageNumbers = elementDefinition.PageNumbers,
            Human = elementDefinition.Human,

            AllowedChildrenIds = elementDefinition.AllowedChildren.Select(x=>x.Id).ToList(),
            AllowedParentsIds = elementDefinition.AllowedParents.Select(x => x.Id).ToList(),

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
            ElementType = new RPGElementType { Id = elementDefinitionDto.ElementTypeId },
            Description = elementDefinitionDto.Description,
            Stat = elementDefinitionDto.Stat,
            PageNumbers = elementDefinitionDto.PageNumbers,
            Human = elementDefinitionDto.Human,
                        

            LevelableData = elementDefinitionDto.LevelableData?.ToModel(),

            PointsContainerScale = elementDefinitionDto.PointsContainerScale,

            Freebies = elementDefinitionDto.Freebies?.Select(x => x.ToModel()).ToList() 
        };
    }
}
