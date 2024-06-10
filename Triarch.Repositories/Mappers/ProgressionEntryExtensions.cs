using Triarch.Database.Models.Definitions;
using Triarch.Dtos.Definitions;

namespace Triarch.Repositories.Mappers;
internal static class ProgressionEntryExtensions
{
    public static ProgressionEntryDto ToDto(this ProgressionEntry progressionEntry)
    {
        return new ProgressionEntryDto
        {
            Id = progressionEntry.Id,
            Text = progressionEntry.Text,
            ProgressionLevel = progressionEntry.ProgressionLevel
        };
    }

    public static ProgressionEntry ToModel(this ProgressionEntryDto progressionEntryDto)
    {
        return new ProgressionEntry
        {
            Id = progressionEntryDto.Id,
            Text = progressionEntryDto.Text,
            ProgressionLevel = progressionEntryDto.ProgressionLevel
        };
    }
}
