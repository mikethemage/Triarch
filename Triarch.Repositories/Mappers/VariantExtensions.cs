using Triarch.Database.Models.Definitions;
using Triarch.Dtos.Definitions;

namespace Triarch.Repositories.Mappers;
internal static class VariantExtensions
{
    public static VariantDefinitionDto ToDto(this VariantDefinition variantDefinition)
    {
        return new VariantDefinitionDto
        {
            Id = variantDefinition.Id,
            VariantName = variantDefinition.VariantName,
            CostPerLevel = variantDefinition.CostPerLevel,
            Description = variantDefinition.Description,
            IsDefault = variantDefinition.IsDefault
        };
    }

    public static VariantDefinition ToModel(this VariantDefinitionDto variantDefinitionDto)
    {
        return new VariantDefinition
        {
            Id = variantDefinitionDto.Id,
            VariantName = variantDefinitionDto.VariantName,
            CostPerLevel = variantDefinitionDto.CostPerLevel,
            Description = variantDefinitionDto.Description,
            IsDefault = variantDefinitionDto.IsDefault
        };
    }
}
