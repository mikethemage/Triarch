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

    public async Task<RPGSystemDto> GetByIdAsync(int id)
    {
        RPGSystem? system = await _context.RPGSystems.Where(x => x.Id == id).Include(x => x.Ruleset).SingleOrDefaultAsync();

        if (system == null)
        {
            throw new RPGSystemNotFoundException($"RPG System not found: {id}");
        }

        RPGSystemDto output = new RPGSystemDto
        {
            Id = system.Id,
            SystemName = system.SystemName,
            DescriptiveName = system.DescriptiveName,
            OwnerUserId = system.OwnerUserId,
            Ruleset = system.Ruleset.ToDto()
        };

        List<Genre> genres = await _context.Genres.Where(x => x.RPGSystemId == system.Id).ToListAsync();

        output.Genres = genres.Select(x =>
                new GenreDto
                {
                    Id = x.Id,
                    GenreName = x.GenreName,
                    GenreOrder = x.GenreOrder,
                }
            ).ToList();

        List<ProgressionDto> progressionDtos = new List<ProgressionDto>();
        List<Progression> progressions = await _context.Progressions.Where(x => x.RPGSystemId == system.Id).ToListAsync();
        foreach (Progression progression in progressions)
        {
            List<ProgressionEntry> progressionEntries = await _context.ProgressionEntries.Where(x => x.ProgressionId == progression.Id).ToListAsync();

            progressionDtos.Add(new ProgressionDto
            {
                Id = progression.Id,
                ProgressionType = progression.ProgressionType,
                CustomProgression = progression.CustomProgression,
                Linear = progression.Linear,
                Progressions = progressionEntries.Select(x => new ProgressionEntryDto
                {
                    Id = x.Id,
                    ProgressionLevel = x.ProgressionLevel,
                    Text = x.Text
                }).ToList()
            });
        }

        output.Progressions = progressionDtos;

        List<RPGElementType> elementTypes = await _context.RPGElementTypes.Where(x => x.RPGSystemId == system.Id).ToListAsync();
        output.ElementTypes = elementTypes.Select(x => new RPGElementTypeDto
        {
            Id = x.Id,
            TypeName = x.TypeName,
            TypeOrder = x.TypeOrder
        }).ToList();

        List<RPGElementDefinitionDto> elementDefinitionDtos = new List<RPGElementDefinitionDto>();

        List<RPGElementDefinition> elementDefinitions = await _context.RPGElementDefinitions.Where(x => x.RPGSystemId == system.Id)
            .Include(x => x.LevelableData)
            .Include(x => x.AllowedChildren)
            .ToListAsync();

        foreach (RPGElementDefinition elementDefinition in elementDefinitions)
        {
            RPGElementDefinitionDto elementDefinitionDto = new RPGElementDefinitionDto
            {
                Id = elementDefinition.Id,
                ElementName = elementDefinition.ElementName,
                ElementTypeName = elementTypes.Where(x => x.Id == elementDefinition.ElementTypeId).First().TypeName,
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
                    Id = elementDefinition.LevelableData.Id,
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
                         progressions.Where(x => x.Id == elementDefinition.LevelableData.ProgressionId).First().ProgressionType;
                }
                if (elementDefinition.LevelableData.VariantDefinitions != null)
                {
                    elementDefinitionDto.LevelableData.Variants = new List<VariantDefinitionDto>();

                    foreach (VariantDefinition variantDefinition in elementDefinition.LevelableData.VariantDefinitions)
                    {
                        elementDefinitionDto.LevelableData.Variants.Add(new VariantDefinitionDto
                        {
                            Id = variantDefinition.Id,
                            VariantName = variantDefinition.VariantName,
                            Description = variantDefinition.Description,
                            IsDefault = variantDefinition.IsDefault,
                            CostPerLevel = variantDefinition.CostPerLevel
                        });
                    }
                }
            }

            foreach (RPGElementDefinition allowedChild in elementDefinition.AllowedChildren)
            {
                elementDefinitionDto.AllowedChildrenNames.Add(allowedChild.ElementName);
            }

            List<RPGFreebie> Freebies = await _context.RPGFreebies.Where(x => x.OwnerElementDefinitionId == elementDefinition.Id).ToListAsync();
            if (Freebies.Count > 0)
            {
                elementDefinitionDto.Freebies = new List<FreebieDto>();
                foreach (RPGFreebie freebie in Freebies)
                {
                    elementDefinitionDto.Freebies.Add(new FreebieDto
                    {
                        Id = freebie.Id,
                        FreeLevels = freebie.FreeLevels,
                        RequiredLevels = freebie.RequiredLevels,
                        FreebieElementDefinitionName = elementDefinitions.Where(x => x.Id == freebie.FreebieElementDefinitionId).First().ElementName
                    });
                }
            }

            elementDefinitionDtos.Add(elementDefinitionDto);
        }

        output.ElementDefinitions = elementDefinitionDtos;

        return output;
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
                SystemName = input.SystemName
            };

            _context.Add(existing);
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
                throw new Exception($"Conflicting System found: {conflict.Id}");
            }
        }

        existing.DescriptiveName = input.DescriptiveName;

        CoreRuleset? ruleset = null;
        if (input.Ruleset.Id != 0)
        {
            ruleset = await _context.CoreRulesets.Where(x => x.Id == input.Ruleset.Id).FirstOrDefaultAsync();
        }
        if (ruleset == null || ruleset.CoreRulesetName != input.Ruleset.CoreRulesetName)
        {
            ruleset = await _context.CoreRulesets.Where(x => x.CoreRulesetName == input.Ruleset.CoreRulesetName).FirstOrDefaultAsync();
        }
        if (ruleset == null)
        {
            ruleset = new CoreRuleset
            {
                CoreRulesetName = input.Ruleset.CoreRulesetName,
                RPGSystems = new List<RPGSystem> { existing }
            };
            _context.Add(ruleset);
        }

        existing.Ruleset = ruleset;
        if (ruleset.Id != 0)
        {
            existing.RulesetId = ruleset.Id;
        }

        List<Genre> genres = new List<Genre>();

        if (existing.Id != 0)
        {
            genres = await _context.Genres.Where(x => x.RPGSystemId == existing.Id).ToListAsync();
        }

        List<GenreDto> genresToProcess = new List<GenreDto>(input.Genres);
        foreach (GenreDto inputGenre in input.Genres.Where(x => x.Id != 0))
        {
            Genre? existingGenre = genres.Where(x => x.Id == inputGenre.Id).FirstOrDefault();
            if (existingGenre != null)
            {
                existingGenre.GenreName = inputGenre.GenreName;
                existingGenre.GenreOrder = inputGenre.GenreOrder;
                genresToProcess.Remove(inputGenre);
            }
        }
        foreach (GenreDto inputGenre in genresToProcess)
        {
            Genre? existingGenre = genres.Where(x => x.GenreName == inputGenre.GenreName).FirstOrDefault();
            if (existingGenre != null)
            {
                existingGenre.GenreOrder = inputGenre.GenreOrder;
            }
            else
            {
                Genre newGenre = new Genre
                {
                    GenreName = inputGenre.GenreName,
                    GenreOrder = inputGenre.GenreOrder,
                    RPGSystem = existing
                };
                if (existing.Id != 0)
                {
                    newGenre.RPGSystemId = existing.Id;
                }
                _context.Add(newGenre);
                existing.Genres.Add(newGenre);
                genres.Add(newGenre);
            }
        }

        List<RPGElementType> elementTypes = new List<RPGElementType>();
        if (existing.Id != 0)
        {
            elementTypes = await _context.RPGElementTypes.Where(x => x.RPGSystemId == existing.Id).ToListAsync();
        }
        List<RPGElementTypeDto> elementTypesToProcess = new List<RPGElementTypeDto>(input.ElementTypes);
        foreach (RPGElementTypeDto inputElementType in input.ElementTypes.Where(x => x.Id != 0))
        {
            RPGElementType? existingElementType = elementTypes.Where(x => x.Id == inputElementType.Id).FirstOrDefault();
            if (existingElementType != null)
            {
                existingElementType.TypeName = inputElementType.TypeName;
                existingElementType.TypeOrder = inputElementType.TypeOrder;
                elementTypesToProcess.Remove(inputElementType);
            }
        }

        foreach (RPGElementTypeDto inputElementType in elementTypesToProcess)
        {
            RPGElementType? existingElementType = elementTypes.Where(x => x.TypeName == inputElementType.TypeName).FirstOrDefault();
            if (existingElementType != null)
            {
                existingElementType.TypeOrder = inputElementType.TypeOrder;
            }
            else
            {
                RPGElementType newElementType = new RPGElementType
                {
                    TypeName = inputElementType.TypeName,
                    TypeOrder = inputElementType.TypeOrder,
                    RPGSystem = existing
                };
                if (existing.Id != 0)
                {
                    newElementType.RPGSystemId = existing.Id;
                }
                _context.Add(newElementType);
                existing.RPGElementTypes.Add(newElementType);
                elementTypes.Add(newElementType);
            }
        }

        List<Progression> progressions = new List<Progression>();
        if (existing.Id != 0)
        {
            progressions = await _context.Progressions.Where(x => x.RPGSystemId == existing.Id).ToListAsync();
        }
        List<ProgressionDto> progressionsToProcess = new List<ProgressionDto>(input.Progressions);
        foreach (ProgressionDto inputProgression in input.Progressions.Where(x => x.Id != 0))
        {
            Progression? existingProgression = progressions.Where(x => x.Id == inputProgression.Id).FirstOrDefault();
            if (existingProgression != null)
            {
                existingProgression.ProgressionType = inputProgression.ProgressionType;
                existingProgression.Linear = inputProgression.Linear;
                existingProgression.CustomProgression = inputProgression.CustomProgression;
                progressionsToProcess.Remove(inputProgression);
                await UpdateProgressionEntries(existingProgression, inputProgression);
            }
        }
        foreach (ProgressionDto inputProgression in progressionsToProcess)
        {
            Progression? existingProgression = progressions.Where(x => x.ProgressionType == inputProgression.ProgressionType).FirstOrDefault();
            if (existingProgression != null)
            {
                existingProgression.Linear = inputProgression.Linear;
                existingProgression.CustomProgression = inputProgression.CustomProgression;
                await UpdateProgressionEntries(existingProgression, inputProgression);
            }
            else
            {
                Progression newProgression = new Progression
                {
                    ProgressionType = inputProgression.ProgressionType,
                    Linear = inputProgression.Linear,
                    CustomProgression = inputProgression.CustomProgression,
                    RPGSystem = existing
                };
                if (existing.Id != 0)
                {
                    newProgression.RPGSystemId = existing.Id;
                }
                _context.Add(newProgression);
                existing.Progressions.Add(newProgression);
                progressions.Add(newProgression);
                await UpdateProgressionEntries(newProgression, inputProgression);
            }
        }

        List<RPGElementDefinition> elementDefinitions = new List<RPGElementDefinition>();
        if (existing.Id != 0)
        {
            elementDefinitions = await _context.RPGElementDefinitions.Where(x => x.RPGSystemId == existing.Id)
                .Include(x=>x.LevelableData)
                .Include(x=>x.AllowedChildren)
                .ToListAsync();
        }

        foreach (RPGElementDefinitionDto elementDefinitionDto in input.ElementDefinitions)
        {
            RPGElementDefinition? existingElementDefinition = null;
            if (elementDefinitionDto.Id!=0)
            {
                existingElementDefinition = elementDefinitions.Where(x => x.Id == elementDefinitionDto.Id).FirstOrDefault();
            }
            if (existingElementDefinition == null)
            {
                existingElementDefinition = elementDefinitions.Where(x => x.ElementName == elementDefinitionDto.ElementName).FirstOrDefault();
            }
            if (existingElementDefinition == null)
            {
                existingElementDefinition = new RPGElementDefinition
                {
                    RPGSystem = existing,
                    AllowedChildren = new List<RPGElementDefinition>()
                };
                if (existing.Id != 0)
                {
                    existingElementDefinition.RPGSystemId = existing.Id;
                }
                elementDefinitions.Add(existingElementDefinition);
                _context.RPGElementDefinitions.Add(existingElementDefinition);
                existing.RPGElementDefinitions.Add(existingElementDefinition);
            }
            
            existingElementDefinition.ElementName= elementDefinitionDto.ElementName;
            existingElementDefinition.PageNumbers = elementDefinitionDto.PageNumbers;
            existingElementDefinition.Description = elementDefinitionDto.Description;
            existingElementDefinition.ElementType = elementTypes.Where(x=>x.TypeName==elementDefinitionDto.ElementTypeName).First();
            existingElementDefinition.Stat= elementDefinitionDto.Stat;
            existingElementDefinition.Human= elementDefinitionDto.Human;
            existingElementDefinition.PointsContainerScale= elementDefinitionDto.PointsContainerScale;
            
            if(elementDefinitionDto.LevelableData!=null)
            {
                if(existingElementDefinition.LevelableData==null)
                {
                    LevelableDefinition newLevelableData = new LevelableDefinition
                    {
                        RPGElementDefinition = existingElementDefinition                        
                    };
                    _context.Add(newLevelableData);
                    existingElementDefinition.LevelableData = newLevelableData;
                }
                existingElementDefinition.LevelableData.CostPerLevel = elementDefinitionDto.LevelableData.CostPerLevel;
                existingElementDefinition.LevelableData.MaxLevel = elementDefinitionDto.LevelableData.MaxLevel;
                existingElementDefinition.LevelableData.EnforceMaxLevel = elementDefinitionDto.LevelableData.EnforceMaxLevel;
                existingElementDefinition.LevelableData.CostPerLevelDescription = elementDefinitionDto.LevelableData.CostPerLevelDescription;
                existingElementDefinition.LevelableData.ProgressionReversed=elementDefinitionDto.LevelableData.ProgressionReversed;
                existingElementDefinition.LevelableData.SpecialPointsPerLevel= elementDefinitionDto.LevelableData.SpecialPointsPerLevel;               
            }
        }


        await _context.SaveChangesAsync();

        return new RPGSystemDto 
        {            
            Id=existing.Id,
            Ruleset = existing.Ruleset.ToDto(),
            SystemName = existing.SystemName,
            DescriptiveName = existing.DescriptiveName,       
            OwnerUserId = existing.OwnerUserId
        };
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
            if(existing==null)
            {
                existing = new ProgressionEntry 
                { 
                    Progression=progression                    
                };
                if(progression.Id!=0)
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

    private static IEnumerable<RPGSystemHeadingDto> ConvertToHeadings(List<RPGSystem> systems)
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
