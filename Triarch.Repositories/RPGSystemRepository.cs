using Microsoft.EntityFrameworkCore;
using Triarch.Database;
using Triarch.Database.Models.Definitions;
using Triarch.Dtos.Definitions;
using Triarch.Repositories.Exceptions;
using Triarch.Repositories.Mappers;

namespace Triarch.Repositories;
public class RPGSystemRepository : IRPGSystemRepository
{
    private readonly TriarchDbContext _context;

    public RPGSystemRepository(TriarchDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RPGSystemHeadingDto>> GetAllAsync()
    {
        var systems = await _context.RPGSystems.Include(x => x.Ruleset).ToListAsync();
        return ConvertToHeadings(systems);
    }

    public async Task<IEnumerable<RPGSystemHeadingDto>> GetAllByUserIdAsync(int userId)
    {
        var systems = await _context.RPGSystems.Include(x => x.Ruleset).Where(x => x.OwnerUserId == userId).ToListAsync();
        return ConvertToHeadings(systems);
    }

    private async Task HydrateSystem(RPGSystem system)
    {
        system.Genres = await _context.Genres.Where(x => x.RPGSystemId == system.Id).ToListAsync();
        system.Progressions = await _context.Progressions.Where(x => x.RPGSystemId == system.Id).ToListAsync();
        foreach (Progression progression in system.Progressions)
        {
            progression.ProgressionEntries = await _context.ProgressionEntries.Where(x => x.ProgressionId == progression.Id).ToListAsync();
        }

        system.RPGElementTypes = await _context.RPGElementTypes.Where(x => x.RPGSystemId == system.Id).ToListAsync();

        system.RPGElementDefinitions = await _context.RPGElementDefinitions.Where(x => x.RPGSystemId == system.Id)
           .Include(x => x.LevelableData)
           .Include(x => x.AllowedChildren)
           .ToListAsync();

        foreach (RPGElementDefinition elementDefinition in system.RPGElementDefinitions)
        {
            if (elementDefinition.LevelableData != null)
            {
                elementDefinition.LevelableData.VariantDefinitions = await _context.VariantDefinitions.Where(x => x.LevelableDefinitionId == elementDefinition.LevelableData.Id).ToListAsync();
            }

            elementDefinition.Freebies = await _context.RPGFreebies.Where(x => x.OwnerElementDefinitionId == elementDefinition.Id).ToListAsync();
        }
    }

    public async Task<RPGSystemDto> GetByIdAsync(int id)
    {
        RPGSystem? system = await _context.RPGSystems.Where(x => x.Id == id).Include(x => x.Ruleset).SingleOrDefaultAsync();

        if (system == null)
        {
            throw new RPGSystemNotFoundException($"RPG System not found: {id}");
        }

        await HydrateSystem(system);

        return system.ToDto();
    }



    public async Task<RPGSystemDto> SaveAsync(RPGSystemDto input)
    {
        RPGSystem? existing = null;
        if (input.Id != 0)
        {
            RPGSystem? existingById = await _context.RPGSystems.Where(x => x.Id == input.Id).FirstOrDefaultAsync();
            if (existingById != null)
            {
                if (existingById.OwnerUserId == input.OwnerUserId)
                {
                    //Overwrite
                    existing = existingById;
                }
            }
        }

        if (existing == null)
        {
            RPGSystem? existingByName = await _context.RPGSystems.Where(x => x.SystemName == input.SystemName && x.OwnerUserId == input.OwnerUserId).FirstOrDefaultAsync();
            if (existingByName != null)
            {
                //Overwrite
                existing = existingByName;
            }
        }

        if (existing == null)
        {
            existing = new RPGSystem
            {
                OwnerUserId = input.OwnerUserId,
                SystemName = input.SystemName,
                DescriptiveName = input.DescriptiveName
            };
            CoreRuleset? existingRuleset = await _context.CoreRulesets.Where(x => x.CoreRulesetName == input.Ruleset.CoreRulesetName).FirstOrDefaultAsync();
            if (existingRuleset == null)
            {
                CoreRuleset coreRuleset = new CoreRuleset
                {
                    CoreRulesetName = input.Ruleset.CoreRulesetName,
                    RPGSystems = new List<RPGSystem> { existing }
                };
                _context.CoreRulesets.Add(coreRuleset);
                existing.Ruleset = coreRuleset;
            }
            else
            {
                existing.Ruleset = existingRuleset;
            }
            _context.RPGSystems.Add(existing);

            existing.Genres = input.Genres.Select(x => new Genre
            {
                GenreName = x.GenreName,
                GenreOrder = x.GenreOrder,
                RPGSystem = existing
            }).ToList();

            _context.AddRange(existing.Genres);

            foreach (ProgressionDto progressionDto in input.Progressions)
            {
                Progression progression = new Progression
                {
                    ProgressionType = progressionDto.ProgressionType,
                    CustomProgression = progressionDto.CustomProgression,
                    Linear = progressionDto.Linear,
                    RPGSystem = existing
                };

                progression.ProgressionEntries = progressionDto.Progressions.Select(x => new ProgressionEntry
                {
                    Progression = progression,
                    ProgressionLevel = x.ProgressionLevel,
                    Text = x.Text
                }).ToList();

                existing.Progressions.Add(progression);
            }
            _context.AddRange(existing.Progressions);

            existing.RPGElementTypes = input.ElementTypes.Select(x => new RPGElementType
            {
                RPGSystem = existing,
                BuiltIn = x.BuiltIn,
                TypeName = x.TypeName,
                TypeOrder = x.TypeOrder
            }).ToList();

            _context.AddRange(existing.RPGElementTypes);

            foreach (RPGElementDefinitionDto elementDefinitionDto in input.ElementDefinitions)
            {
                RPGElementDefinition elementDefinition = new RPGElementDefinition
                {
                    ElementName = elementDefinitionDto.ElementName,
                    Description = elementDefinitionDto.Description,
                    Human = elementDefinitionDto.Human,
                    PageNumbers = elementDefinitionDto.PageNumbers,
                    Stat = elementDefinitionDto.Stat,
                    PointsContainerScale = elementDefinitionDto.PointsContainerScale,
                    RPGSystem = existing
                };

                RPGElementType? elementType = existing.RPGElementTypes.Where(x => x.TypeName == elementDefinitionDto.ElementTypeName).FirstOrDefault();
                if (elementType != null)
                {
                    elementDefinition.ElementType = elementType;
                }

                if (elementDefinitionDto.LevelableData != null)
                {
                    LevelableDefinition levelableDefinition = new LevelableDefinition
                    {
                        CostPerLevel = elementDefinitionDto.LevelableData.CostPerLevel,
                        EnforceMaxLevel = elementDefinitionDto.LevelableData.EnforceMaxLevel,
                        CostPerLevelDescription = elementDefinitionDto.LevelableData.CostPerLevelDescription,
                        MaxLevel = elementDefinitionDto.LevelableData.MaxLevel,
                        ProgressionReversed = elementDefinitionDto.LevelableData.ProgressionReversed,
                        SpecialPointsPerLevel = elementDefinitionDto.LevelableData.SpecialPointsPerLevel,
                        RPGElementDefinition = elementDefinition
                    };

                    if (elementDefinitionDto.LevelableData.ProgressionName != null)
                    {
                        Progression? progression = existing.Progressions.Where(x => x.ProgressionType == elementDefinitionDto.LevelableData.ProgressionName).FirstOrDefault();
                        if (progression != null)
                        {
                            levelableDefinition.Progression = progression;
                        }
                    }
                    if (elementDefinitionDto.LevelableData.MultiGenreCostPerLevels != null && elementDefinitionDto.LevelableData.MultiGenreCostPerLevels.Count > 0)
                    {
                        levelableDefinition.GenreCostPerLevels = new List<GenreCostPerLevel>();

                        foreach (GenreCostPerLevelDto genreCostPerLevelDto in elementDefinitionDto.LevelableData.MultiGenreCostPerLevels)
                        {
                            GenreCostPerLevel genreCostPerLevel = new GenreCostPerLevel
                            {
                                CostPerLevel = genreCostPerLevelDto.CostPerLevel,
                                Levelable = levelableDefinition
                            };

                            Genre? genre = existing.Genres.Where(x => x.GenreName == genreCostPerLevelDto.GenreName).FirstOrDefault();
                            if (genre != null)
                            {
                                genreCostPerLevel.Genre = genre;
                            }
                            levelableDefinition.GenreCostPerLevels.Add(genreCostPerLevel);
                        }
                        _context.AddRange(levelableDefinition.GenreCostPerLevels);
                    }

                    if(elementDefinitionDto.LevelableData.Variants != null && elementDefinitionDto.LevelableData.Variants.Count>0)
                    {
                        levelableDefinition.VariantDefinitions = elementDefinitionDto.LevelableData.Variants.Select(x=>new VariantDefinition {
                            VariantName=x.VariantName,
                            Description=x.Description,
                            CostPerLevel=x.CostPerLevel,
                            IsDefault=x.IsDefault,
                            LevelableDefinition= levelableDefinition
                            
                        }).ToList();

                        _context.AddRange(levelableDefinition.VariantDefinitions);
                    }

                    elementDefinition.LevelableData = levelableDefinition;
                }
                _context.Add(elementDefinition);
                existing.RPGElementDefinitions.Add(elementDefinition);
            }

            foreach (RPGElementDefinitionDto elementDefinitionDto in input.ElementDefinitions.Where(x=>x.AllowedChildrenNames != null && x.AllowedChildrenNames.Count>0))
            {
                RPGElementDefinition parent = existing.RPGElementDefinitions.Where(x=>x.ElementName== elementDefinitionDto.ElementName).First();
                List<RPGElementDefinition> children = existing.RPGElementDefinitions.Where(x=>elementDefinitionDto.AllowedChildrenNames.Contains(x.ElementName)).ToList();
                parent.AllowedChildren= children;
            }
        }
        else
        {
            RPGSystem? conflict = await _context.RPGSystems.Where(x =>
                (
                // Is Built-In System OR
                x.OwnerUserId == input.OwnerUserId) &&
                x.SystemName == input.SystemName &&
                x.Id != existing.Id).FirstOrDefaultAsync();
            if (conflict != null)
            {
                throw new RPGSystemConflictException($"Conflicting System found: {conflict.Id}");
            }
        }

        await _context.SaveChangesAsync();

        return existing.ToDto();
    }

    private async Task UpdateProgressionEntries(Progression progression, ProgressionDto inputProgression)
    {
        List<ProgressionEntry> progressionEntries = new List<ProgressionEntry>();
        if (progression.Id != 0)
        {
            progressionEntries = await _context.ProgressionEntries.Where(x => x.ProgressionId == progression.Id).ToListAsync();
        }

        foreach (ProgressionEntryDto inputProgressionEntry in inputProgression.Progressions)
        {
            ProgressionEntry? existing = null;
            if (inputProgressionEntry.Id != 0)
            {
                existing = progressionEntries.Where(x => x.Id == inputProgressionEntry.Id).FirstOrDefault();
            }
            if (existing == null)
            {
                existing = progressionEntries.Where(x => x.ProgressionLevel == inputProgressionEntry.ProgressionLevel).FirstOrDefault();
            }
            if (existing == null)
            {
                existing = new ProgressionEntry
                {
                    Progression = progression
                };
                if (progression.Id != 0)
                {
                    existing.ProgressionId = progression.Id;
                }
                progression.ProgressionEntries.Add(existing);
                progressionEntries.Add(existing);
                _context.ProgressionEntries.Add(existing);
            }
            existing.ProgressionLevel = inputProgressionEntry.ProgressionLevel;
            existing.Text = inputProgressionEntry.Text;
        }

    }

    public async Task DeleteAsync(int id)
    {
        RPGSystem? system = await _context.RPGSystems.FirstOrDefaultAsync(x => x.Id == id);
        if (system == null)
        {
            throw new RPGSystemNotFoundException($"RPGSystem not found: {id}");
        }

        List<RPGElementDefinition> elementDefinitions = await _context.RPGElementDefinitions
            .Where(x => x.RPGSystemId == system.Id)
            .Include(x => x.LevelableData)
            .Include(x => x.AllowedChildren)
            .ToListAsync();
        List<RPGFreebie> freebies = new List<RPGFreebie>();
        List<VariantDefinition> variants = new List<VariantDefinition>();
        foreach (RPGElementDefinition elementDefinition in elementDefinitions)
        {
            freebies.AddRange(await _context.RPGFreebies.Where(x => x.OwnerElementDefinitionId == elementDefinition.Id || x.FreebieElementDefinitionId == elementDefinition.Id).ToListAsync());
            if (elementDefinition.LevelableData != null)
            {
                variants.AddRange(await _context.VariantDefinitions.Where(x => x.LevelableDefinitionId == elementDefinition.LevelableData.Id).ToListAsync());
                _context.Remove(elementDefinition.LevelableData);
            }
            elementDefinition.AllowedChildren.Clear();
        }

        freebies = freebies.DistinctBy(x => x.Id).ToList();
        _context.RemoveRange(freebies);
        variants = variants.DistinctBy(x => x.Id).ToList();
        _context.RemoveRange(variants);

        _context.RemoveRange(elementDefinitions);

        _context.RemoveRange(await _context.Genres.Where(x => x.RPGSystemId == system.Id).ToListAsync());
        _context.RemoveRange(await _context.Progressions.Where(x => x.RPGSystemId == system.Id).ToListAsync());

        _context.Remove(system);

        await _context.SaveChangesAsync();
    }

    private static List<RPGSystemHeadingDto> ConvertToHeadings(List<RPGSystem> systems)
    {
        var systemDtos = new List<RPGSystemHeadingDto>();
        foreach (RPGSystem? system in systems)
        {
            systemDtos.Add(
                new RPGSystemHeadingDto
                {
                    Id = system.Id,
                    CoreRulesetName = system.Ruleset.CoreRulesetName,
                    SystemName = system.SystemName
                }
            );
        }
        return systemDtos;
    }
}
