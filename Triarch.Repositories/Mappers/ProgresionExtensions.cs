using Triarch.Database.Models.Ruleset;
using Triarch.Dtos.Ruleset;

namespace Triarch.Repositories.Mappers;
internal static class ProgresionExtensions
{
    public static ProgressionDto ToDto(this Progression progression)
    {
        return new ProgressionDto
        {
            Id = progression.Id,
            ProgressionType = progression.ProgressionType,
            CustomProgression = progression.CustomProgression,
            Progressions = progression.Progressions.Select(x => x.ToDto()).ToList()
        };
    }

    public static Progression ToModel(this ProgressionDto progressionDto)
    {
        return new Progression
        {
            Id = progressionDto.Id,
            ProgressionType = progressionDto.ProgressionType,
            CustomProgression = progressionDto.CustomProgression,
            Progressions = progressionDto.Progressions.Select(x => x.ToModel()).ToList()
        };
    }
}
