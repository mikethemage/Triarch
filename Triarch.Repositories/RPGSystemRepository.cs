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
            
                elementDefinition.LevelableData.GenreCostPerLevels=await _context.GenreCostPerLevels.Where(x=>x.LevelableId==elementDefinition.LevelableData.Id).ToListAsync();
            }

            elementDefinition.Freebies = await _context.RPGFreebies.Where(x => x.OwnerElementDefinitionId == elementDefinition.Id).ToListAsync();
        }
    }

    public async Task<RPGSystemDto> GetByNameAsync(string name, int userId)
    {
        RPGSystem? system = await _context.RPGSystems.Where(x => x.SystemName == name && x.OwnerUserId == userId).Include(x => x.Ruleset).SingleOrDefaultAsync();

        if (system == null)
        {
            throw new RPGSystemNotFoundException($"RPG System not found: {name}");
        }

        await HydrateSystem(system);

        return system.ToDto();
    }

    public async Task<RPGSystemDto> SaveAsync(RPGSystemDto input)
    {
        RPGSystem? existing = await _context.RPGSystems.Where(x => x.SystemName == input.SystemName && x.OwnerUserId == input.OwnerUserId).FirstOrDefaultAsync();       

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

                    if (elementDefinitionDto.LevelableData.Variants != null && elementDefinitionDto.LevelableData.Variants.Count > 0)
                    {
                        levelableDefinition.VariantDefinitions = elementDefinitionDto.LevelableData.Variants.Select(x => new VariantDefinition
                        {
                            VariantName = x.VariantName,
                            Description = x.Description,
                            CostPerLevel = x.CostPerLevel,
                            IsDefault = x.IsDefault,
                            LevelableDefinition = levelableDefinition

                        }).ToList();

                        _context.AddRange(levelableDefinition.VariantDefinitions);
                    }

                    elementDefinition.LevelableData = levelableDefinition;
                }
                
                _context.Add(elementDefinition);
                existing.RPGElementDefinitions.Add(elementDefinition);
            }

            foreach (RPGElementDefinitionDto elementDefinitionDto in input.ElementDefinitions.Where(x => x.AllowedChildrenNames != null && x.AllowedChildrenNames.Count > 0))
            {
                RPGElementDefinition parent = existing.RPGElementDefinitions.Where(x => x.ElementName == elementDefinitionDto.ElementName).First();
                List<RPGElementDefinition> children = existing.RPGElementDefinitions.Where(x => elementDefinitionDto.AllowedChildrenNames.Contains(x.ElementName)).ToList();
                parent.AllowedChildren = children;
            }

            foreach (RPGElementDefinitionDto elementDefinitionDto in input.ElementDefinitions.Where(x=>x.Freebies!=null && x.Freebies.Count>0))
            {
                RPGElementDefinition? ownerElement = existing.RPGElementDefinitions.Where(x => x.ElementName == elementDefinitionDto.ElementName).FirstOrDefault();
                if(ownerElement != null)
                {
                    foreach (FreebieDto freebieDto in elementDefinitionDto.Freebies!)
                    {
                        RPGElementDefinition? freebieElement = existing.RPGElementDefinitions.Where(x => x.ElementName == freebieDto.FreebieElementDefinitionName).FirstOrDefault();
                        if (freebieElement != null)
                        {
                            RPGFreebie freebie = new RPGFreebie
                            {
                                FreebieElementDefinition = freebieElement,
                                FreeLevels = freebieDto.FreeLevels,
                                RequiredLevels = freebieDto.RequiredLevels,
                                OwnerElementDefinition = ownerElement
                            };
                            _context.Add(freebie);
                            ownerElement.Freebies.Add(freebie);
                        }
                    }
                }    
            }

            await _context.SaveChangesAsync();
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

            existing = await _context.RPGSystems.Where(x => x.Id == existing.Id).Include(x => x.Ruleset).SingleAsync();

            if (input.Ruleset.CoreRulesetName != existing.Ruleset.CoreRulesetName)
            {
                CoreRuleset? existingRuleset = await _context.CoreRulesets.Where(x => x.CoreRulesetName == input.Ruleset.CoreRulesetName).FirstOrDefaultAsync();
                if (existingRuleset == null)
                {
                    existingRuleset = new CoreRuleset
                    {
                        CoreRulesetName = input.Ruleset.CoreRulesetName,
                        RPGSystems = [existing]
                    };
                    _context.CoreRulesets.Add(existingRuleset);
                }
                existing.Ruleset = existingRuleset;
            }

            Dictionary<RPGElementDefinitionDto, RPGElementDefinition> matchedDefinitions = new Dictionary<RPGElementDefinitionDto, RPGElementDefinition>();
            List<RPGElementDefinition> toDeleteDefinitions = await _context.RPGElementDefinitions
                                                                .Where(x => x.RPGSystemId == existing.Id)                                                                
                                                                .Include(x => x.AllowedChildren)
                                                                .Include(x=>x.Freebies)
                                                                .Include(x => x.LevelableData)                                                                
                                                                .ToListAsync();
            List<RPGElementDefinitionDto> toAddDefinitions = new List<RPGElementDefinitionDto>(input.ElementDefinitions);            

            //Match on Name:
            foreach (RPGElementDefinitionDto toAddDto in new List<RPGElementDefinitionDto>(toAddDefinitions))
            {
                RPGElementDefinition? toAdd = toDeleteDefinitions.Where(x => x.ElementName == toAddDto.ElementName).FirstOrDefault();
                if (toAdd != null)
                {
                    matchedDefinitions.Add(toAddDto, toAdd);
                    toAddDefinitions.Remove(toAddDto);
                    toDeleteDefinitions.Remove(toAdd);
                }
            }

            foreach (RPGElementDefinition toDelete in toDeleteDefinitions)
            {
                //Remove children first:
                toDelete.AllowedChildren.Clear();
                foreach (RPGElementDefinition toRemoveChild in matchedDefinitions.Values.Where(x => x.AllowedChildren.Contains(toDelete)).ToList())
                {
                    toRemoveChild.AllowedChildren.Remove(toDelete);
                }

                //Remove definition:
                existing.RPGElementDefinitions.Remove(toDelete);
                _context.Remove(toDelete);
            }

            List<RPGElementTypeDto> toAddTypes = new List<RPGElementTypeDto>(input.ElementTypes);
            List<RPGElementType> toDeleteTypes = await _context.RPGElementTypes.Where(x => x.RPGSystemId == existing.Id).ToListAsync();
            Dictionary<RPGElementTypeDto, RPGElementType> matchedTypes = new Dictionary<RPGElementTypeDto, RPGElementType>();            

            foreach (RPGElementTypeDto toAddDto in new List<RPGElementTypeDto>(toAddTypes))
            {
                RPGElementType? toAdd = toDeleteTypes.Where(x => x.TypeName == toAddDto.TypeName).FirstOrDefault();
                if (toAdd != null)
                {
                    matchedTypes.Add(toAddDto, toAdd);
                    toAddTypes.Remove(toAddDto);
                    toDeleteTypes.Remove(toAdd);
                }
            }

            foreach (KeyValuePair<RPGElementTypeDto, RPGElementType> matchedType in matchedTypes)
            {
                if (matchedType.Key.TypeName != matchedType.Value.TypeName)
                {
                    //Update names for existing types:
                    matchedType.Value.TypeName = matchedType.Key.TypeName;
                }
            }

            foreach (RPGElementTypeDto toAddDto in toAddTypes)
            {
                RPGElementType toAdd = new RPGElementType
                {
                    TypeName = toAddDto.TypeName,
                    BuiltIn = toAddDto.BuiltIn,
                    TypeOrder = toAddDto.TypeOrder,
                    RPGSystem = existing
                };
                matchedTypes.Add(toAddDto, toAdd);
                _context.Add(toAdd);
            }

            List<ProgressionDto> toAddProgressions = new List<ProgressionDto>(input.Progressions);
            List<Progression> toDeleteProgressions = await _context.Progressions.Where(x => x.RPGSystemId == existing.Id).Include(x => x.ProgressionEntries).ToListAsync();
            Dictionary<ProgressionDto, Progression> matchedProgressions = new Dictionary<ProgressionDto, Progression>();
            
            foreach (ProgressionDto toAddDto in new List<ProgressionDto>(toAddProgressions))
            {
                Progression? toAdd = toDeleteProgressions.Where(x => x.ProgressionType == toAddDto.ProgressionType).FirstOrDefault();
                if (toAdd != null)
                {
                    matchedProgressions.Add(toAddDto, toAdd);
                    toAddProgressions.Remove(toAddDto);
                    toDeleteProgressions.Remove(toAdd);
                }
            }

            foreach (KeyValuePair<ProgressionDto, Progression> matchedProgression in matchedProgressions)
            {
                if (matchedProgression.Key.ProgressionType != matchedProgression.Value.ProgressionType)
                {
                    //Update names for existing types:
                    matchedProgression.Value.ProgressionType = matchedProgression.Key.ProgressionType;
                }
                if (matchedProgression.Key.Linear != matchedProgression.Value.Linear)
                {
                    //Update names for existing types:
                    matchedProgression.Value.Linear = matchedProgression.Key.Linear;
                }
                if (matchedProgression.Key.CustomProgression != matchedProgression.Value.CustomProgression)
                {
                    //Update names for existing types:
                    matchedProgression.Value.CustomProgression = matchedProgression.Key.CustomProgression;
                }

                List<int> validLevels = matchedProgression.Key.Progressions.Select(x => x.ProgressionLevel).ToList();
                List<ProgressionEntry> progressionEntriesToRemove = matchedProgression.Value.ProgressionEntries.Where(x => !validLevels.Contains(x.ProgressionLevel)).ToList();
                foreach (ProgressionEntry toRemove in progressionEntriesToRemove)
                {
                    matchedProgression.Value.ProgressionEntries.Remove(toRemove);
                }

                foreach (ProgressionEntry existingProgressionEntry in matchedProgression.Value.ProgressionEntries)
                {
                    ProgressionEntryDto? progressionEntryDto = matchedProgression.Key.Progressions.Where(x => x.ProgressionLevel == existingProgressionEntry.ProgressionLevel).FirstOrDefault();
                    if (progressionEntryDto != null)
                    {
                        existingProgressionEntry.Text = progressionEntryDto.Text;
                    }
                }

                List<int> existingProgressionEntries = matchedProgression.Value.ProgressionEntries.Select(x => x.ProgressionLevel).ToList();
                List<ProgressionEntryDto> progresionEntriesToAddDtos = matchedProgression.Key.Progressions.Where(x => !existingProgressionEntries.Contains(x.ProgressionLevel)).ToList();
                foreach (ProgressionEntryDto item in progresionEntriesToAddDtos)
                {
                    ProgressionEntry toAdd = new ProgressionEntry
                    {
                        Progression = matchedProgression.Value,
                        ProgressionLevel = item.ProgressionLevel,
                        Text = item.Text
                    };

                    matchedProgression.Value.ProgressionEntries.Add(
                        new ProgressionEntry
                        {
                            Progression = matchedProgression.Value,
                            ProgressionLevel = item.ProgressionLevel,
                            Text = item.Text
                        }
                        );
                    _context.Add(toAdd);
                }
            }

            foreach (ProgressionDto toAddDto in toAddProgressions)
            {
                var toAdd = new Progression
                {
                    CustomProgression = toAddDto.CustomProgression,
                    Linear = toAddDto.Linear,
                    ProgressionType = toAddDto.ProgressionType,
                    RPGSystem = existing,
                    ProgressionEntries = toAddDto.Progressions.Select(x => new ProgressionEntry { ProgressionLevel = x.ProgressionLevel, Text = x.Text }).ToList()
                };

                matchedProgressions.Add(toAddDto,toAdd);
                _context.Add(toAdd);
                existing.Progressions.Add(toAdd);
            }

            foreach (KeyValuePair<RPGElementDefinitionDto, RPGElementDefinition> matchedDefinition in matchedDefinitions)
            {
                matchedDefinition.Value.Description = matchedDefinition.Key.Description;
                matchedDefinition.Value.Human=matchedDefinition.Key.Human;
                matchedDefinition.Value.PageNumbers = matchedDefinition.Key.PageNumbers;
                matchedDefinition.Value.PointsContainerScale = matchedDefinition.Key.PointsContainerScale;
                matchedDefinition.Value.Stat=matchedDefinition.Key.Stat;

                //Update all the element types:
                RPGElementTypeDto? changedType = matchedTypes.Keys.Where(x => x.TypeName == matchedDefinition.Key.ElementTypeName).FirstOrDefault();
                if (changedType != null)
                {
                    if (matchedDefinition.Value.ElementTypeId == 0 || matchedDefinition.Value.ElementTypeId != matchedTypes[changedType].Id)
                    {
                        matchedDefinition.Value.ElementType = matchedTypes[changedType];
                    }
                }

                if (matchedDefinition.Key.LevelableData != null)
                {
                    if (matchedDefinition.Value.LevelableData == null)
                    {
                        matchedDefinition.Value.LevelableData = new LevelableDefinition
                        {
                            RPGElementDefinition = matchedDefinition.Value
                        };
                    }

                    matchedDefinition.Value.LevelableData.CostPerLevel = matchedDefinition.Key.LevelableData.CostPerLevel;
                    matchedDefinition.Value.LevelableData.CostPerLevelDescription = matchedDefinition.Key.LevelableData.CostPerLevelDescription;
                    matchedDefinition.Value.LevelableData.MaxLevel = matchedDefinition.Key.LevelableData.MaxLevel;
                    matchedDefinition.Value.LevelableData.EnforceMaxLevel= matchedDefinition.Key.LevelableData.EnforceMaxLevel;                    
                    matchedDefinition.Value.LevelableData.SpecialPointsPerLevel= matchedDefinition.Key.LevelableData.SpecialPointsPerLevel;
                    matchedDefinition.Value.LevelableData.ProgressionReversed=matchedDefinition.Key.LevelableData.ProgressionReversed;

                    ProgressionDto? changedProgression = matchedProgressions.Keys.Where(x => x.ProgressionType == matchedDefinition.Key.LevelableData.ProgressionName).FirstOrDefault();
                    if (changedProgression != null)
                    {
                        if (matchedDefinition.Value.LevelableData.ProgressionId == 0 || matchedDefinition.Value.LevelableData.ProgressionId != matchedProgressions[changedProgression].Id)
                        {
                            matchedDefinition.Value.LevelableData.Progression = matchedProgressions[changedProgression];
                        }
                    }
                    List<VariantDefinitionDto> VariantsToAdd;
                    List<VariantDefinition> VariantsToDelete = await _context.VariantDefinitions.Where(x=>x.LevelableDefinitionId==matchedDefinition.Value.LevelableData.Id).ToListAsync();
                    if(matchedDefinition.Key.LevelableData.Variants==null)
                    {
                        VariantsToAdd = new List<VariantDefinitionDto>();
                    }
                    else
                    {
                        VariantsToAdd = new List<VariantDefinitionDto>(matchedDefinition.Key.LevelableData.Variants);
                    }

                    Dictionary<VariantDefinitionDto, VariantDefinition> matchedVariants = new Dictionary<VariantDefinitionDto, VariantDefinition>();                    

                    foreach (VariantDefinitionDto toAddDto in new List<VariantDefinitionDto>(VariantsToAdd))
                    {
                        VariantDefinition? toAdd = VariantsToDelete.Where(x => x.VariantName == toAddDto.VariantName).FirstOrDefault();
                        if (toAdd != null)
                        {
                            matchedVariants.Add(toAddDto, toAdd);
                            VariantsToAdd.Remove(toAddDto);
                            VariantsToDelete.Remove(toAdd);
                        }
                    }

                    foreach (VariantDefinition toDelete in VariantsToDelete)
                    {
                        toDelete.LevelableDefinition = null;
                        _context.Remove(toDelete);                        
                    }

                    foreach (KeyValuePair<VariantDefinitionDto, VariantDefinition> matchedVariant in matchedVariants)
                    {
                        if(matchedVariant.Value.VariantName!=matchedVariant.Key.VariantName)
                        {
                            matchedVariant.Value.VariantName = matchedVariant.Key.VariantName;
                        }

                        matchedVariant.Value.CostPerLevel = matchedVariant.Key.CostPerLevel;
                        matchedVariant.Value.Description = matchedVariant.Key.Description;
                        matchedVariant.Value.IsDefault = matchedVariant.Key.IsDefault;
                    }

                    foreach (VariantDefinitionDto toAddDto in VariantsToAdd)
                    {
                        VariantDefinition toAdd = new VariantDefinition
                        {
                            LevelableDefinition = matchedDefinition.Value.LevelableData,
                            CostPerLevel = toAddDto.CostPerLevel,
                            Description = toAddDto.Description,
                            IsDefault = toAddDto.IsDefault,
                            VariantName = toAddDto.VariantName
                        };

                        _context.Add(toAdd);
                        matchedDefinition.Value.LevelableData.VariantDefinitions.Add(toAdd);
                        matchedVariants.Add(toAddDto, toAdd);
                    }


                }
                else
                {
                    if (matchedDefinition.Value.LevelableData != null)
                    {
                        matchedDefinition.Value.LevelableData = null;
                    }
                }

            }

            foreach (Progression toDelete in toDeleteProgressions)
            {
                _context.Remove(toDelete);
            }

            //Remove types:
            foreach (RPGElementType toDelete in toDeleteTypes)
            {
                _context.Remove(toDelete);
            }

            foreach (RPGElementDefinitionDto elementDefinitionDto in input.ElementDefinitions.Where(x => x.Freebies != null && x.Freebies.Count > 0))
            {
                RPGElementDefinition? ownerElement = existing.RPGElementDefinitions.Where(x => x.ElementName == elementDefinitionDto.ElementName).FirstOrDefault();
                if (ownerElement != null)
                {
                    foreach(RPGFreebie freebie in new List<RPGFreebie>(ownerElement.Freebies))
                    {
                        if(!elementDefinitionDto.Freebies!.Any(x=>x.FreebieElementDefinitionName==freebie.FreebieElementDefinition.ElementName))
                        {
                            ownerElement.Freebies.Remove(freebie);
                            _context.Remove(freebie);
                        }
                    }

                    foreach (FreebieDto freebieDto in elementDefinitionDto.Freebies!)
                    {
                        RPGElementDefinition? freebieElement = existing.RPGElementDefinitions.Where(x => x.ElementName == freebieDto.FreebieElementDefinitionName).FirstOrDefault();
                        if (freebieElement != null)
                        {
                            RPGFreebie? freebie = ownerElement.Freebies.Where(x => x.FreebieElementDefinition.ElementName == freebieElement.ElementName).FirstOrDefault();
                            if (freebie != null)
                            {
                                freebie.FreeLevels = freebieDto.FreeLevels;
                                freebie.RequiredLevels = freebieDto.RequiredLevels;
                            }
                            else
                            {
                                freebie = new RPGFreebie
                                {
                                    FreebieElementDefinition = freebieElement,
                                    FreeLevels = freebieDto.FreeLevels,
                                    RequiredLevels = freebieDto.RequiredLevels,
                                    OwnerElementDefinition = ownerElement
                                };
                                _context.Add(freebie);
                                ownerElement.Freebies.Add(freebie);
                            }
                        }
                    }
                }
            }

            await _context.SaveChangesAsync();

            RPGSystem newSystem = await _context.RPGSystems.Where(x=>x.Id==existing.Id).Include(x => x.Ruleset).SingleAsync();

            await HydrateSystem(newSystem);
            existing = newSystem;
        }        

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
            ProgressionEntry? existing = progressionEntries.Where(x => x.ProgressionLevel == inputProgressionEntry.ProgressionLevel).FirstOrDefault();
            
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

    public async Task DeleteAsync(string name, int userId)
    {
        RPGSystem? system = await _context.RPGSystems.FirstOrDefaultAsync(x => x.SystemName == name && x.OwnerUserId == userId);
        if (system == null)
        {
            throw new RPGSystemNotFoundException($"RPGSystem not found: {name}");
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
                    CoreRulesetName = system.Ruleset.CoreRulesetName,
                    SystemName = system.SystemName
                }
            );
        }
        return systemDtos;
    }
}
