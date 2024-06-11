using Triarch.Database.Models.Definitions;
using Triarch.Dtos.Definitions;

namespace Triarch.Repositories.Mappers;
internal static class ElementTypeExtensions
{
    public static RPGElementTypeDto ToDto(this RPGElementType rPGElementType)
    {
        return new RPGElementTypeDto
        {
            Id = rPGElementType.Id,
            TypeName = rPGElementType.TypeName,
            TypeOrder = rPGElementType.TypeOrder
        };
    }

    public static RPGElementType ToModel(this RPGElementTypeDto rPGElementTypeDto)
    {
        return new RPGElementType
        {
            Id = rPGElementTypeDto.Id,
            TypeName = rPGElementTypeDto.TypeName,
            TypeOrder = rPGElementTypeDto.TypeOrder
        };
    }
}
