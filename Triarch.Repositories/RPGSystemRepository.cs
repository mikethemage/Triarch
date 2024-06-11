﻿using Microsoft.EntityFrameworkCore;
using Triarch.Database;
using Triarch.Database.Models.Definitions;
using Triarch.Dtos.Definitions;
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
        var systems = await _context.RPGSystems.Include(x => x.Ruleset).Where(x=>x.OwnerUserId == userId).ToListAsync();
        return ConvertToHeadings(systems);
    }

    public async Task<RPGSystemDto> GetByIdAsync(int id)
    {
        RPGSystem? system = await _context.RPGSystems.Where(x => x.Id == id).Include(x => x.Ruleset).SingleOrDefaultAsync();

        if (system == null)
        {
            throw new Exception("RPG System not found");
        }

        await HydrateSystem(system);

        return system.ToDto();
    }

    private async Task HydrateSystem(RPGSystem system)
    {
        system.ElementTypes = await _context.RPGElementTypes.Where(x => x.RPGSystem == system).ToListAsync();
        system.ElementDefinitions = await _context.RPGElementDefinitions.Where(x => x.RPGSystem == system)
            .Include(x => x.ElementType)
            .Include(x => x.LevelableData)            
            .ToListAsync();

        foreach (RPGElementDefinition element in system.ElementDefinitions)
        {
            ICollection<RPGElementDefinition> allowedChildren = await _context.RPGElementDefinitions.Where(x => x.Id == element.Id).Include(x => x.AllowedChildren).Select(x => x.AllowedChildren).SingleAsync();
            element.AllowedChildren = system.ElementDefinitions.Where(x => allowedChildren.Select(y => y.Id).Contains(x.Id)).ToList();

            ICollection<RPGElementDefinition> allowedParents = await _context.RPGElementDefinitions.Where(x => x.Id == element.Id).Include(x => x.AllowedParents).Select(x => x.AllowedParents).SingleAsync();
            element.AllowedParents = system.ElementDefinitions.Where(x => allowedParents.Select(y => y.Id).Contains(x.Id)).ToList();

            element.Freebies = await _context.RPGFreebies.Where(x => x.OwnerElementDefinitionId == element.Id).ToListAsync();

            foreach (Freebie freebie in element.Freebies)
            {
                RPGElementDefinition? freebieElementDefinition = system.ElementDefinitions.Where(x => x.Id == freebie.FreebieElementDefinitionId).FirstOrDefault();
                if (freebieElementDefinition != null)
                {
                    freebie.FreebieElementDefinition = freebieElementDefinition;
                }
            }            
        }

        system.Genres = await _context.Genres.Where(x => x.RPGSystem == system).ToListAsync();

        system.Progressions = await _context.Progressions.Where(x => x.RPGSystem == system).ToListAsync();

        foreach (Progression progression in system.Progressions)
        {
            progression.Progressions = await _context.ProgressionEntries.Where(x => x.Progression == progression).ToListAsync();            
        }
    }

    public async Task SaveAsync(RPGSystemDto rPGSystemDto)
    {
        RPGSystem rPGSystem = rPGSystemDto.ToModel();
        if(rPGSystem.Id == 0)
        {
            _context.RPGSystems.Add(rPGSystem);
        }
        else
        {
            RPGSystem? existing = await _context.RPGSystems.FindAsync(rPGSystem.Id);
            if (existing == null)
            {
                _context.RPGSystems.Add(rPGSystem);
            }
            else
            {
                await HydrateSystem(existing);
                _context.Entry(existing).CurrentValues.SetValues(rPGSystem);

                foreach (Genre genre in existing.Genres)
                {
                    if (rPGSystem.Genres.Select(x => x.Id).Contains(genre.Id))
                    {
                        _context.Entry(genre).CurrentValues.SetValues(rPGSystem.Genres.First(x => x.Id == genre.Id));
                    }
                    else
                    {
                        rPGSystem.Genres.Remove(genre);
                        _context.Remove(genre);
                    }
                }
                foreach (Genre newGenre in rPGSystem.Genres.Where(x => x.Id == 0))
                {
                    existing.Genres.Add(newGenre);
                    _context.Add(newGenre);
                }

                foreach (Progression progression in existing.Progressions.ToList())
                {
                    if(rPGSystem.Progressions.Select(x=>x.Id).Contains(progression.Id))
                    {
                        Progression updatedProgression = rPGSystem.Progressions.First(x => x.Id == progression.Id);
                        _context.Entry(progression).CurrentValues.SetValues(updatedProgression);

                        foreach (ProgressionEntry progressionEntry in progression.Progressions.ToList())
                        {
                            if (updatedProgression.Progressions.Select(x => x.Id).Contains(progressionEntry.Id))
                            {
                                _context.Entry(progressionEntry).CurrentValues.SetValues(updatedProgression.Progressions.First(x => x.Id == progressionEntry.Id));
                            }
                            else
                            {
                                progression.Progressions.Remove(progressionEntry);
                                _context.Remove(progressionEntry);
                            }
                        }

                        foreach (ProgressionEntry newProgressionEntry in updatedProgression.Progressions.Where(x=>x.Id==0))
                        {
                            progression.Progressions.Add(newProgressionEntry);
                            _context.Add(newProgressionEntry);
                        }
                    }
                    else
                    {
                        rPGSystem.Progressions.Remove(progression);
                        _context.Remove(progression);
                    }
                }
                foreach (Progression newProgression in rPGSystem.Progressions.Where(x=>x.Id==0))
                {
                    existing.Progressions.Add(newProgression);
                    _context.Add(newProgression);
                }

                foreach (RPGElementType elementType in existing.ElementTypes)
                {
                    if (rPGSystem.ElementTypes.Select(x=>x.Id).Contains(elementType.Id))
                    {
                        _context.Entry(elementType).CurrentValues.SetValues(rPGSystem.ElementTypes.First(x => x.Id == elementType.Id));
                    }
                    else
                    {
                        rPGSystem.ElementTypes.Remove(elementType);
                        _context.Remove(elementType);
                    }
                }
                foreach (RPGElementType newElementType in rPGSystem.ElementTypes.Where(x=>x.Id==0))
                {
                    existing.ElementTypes.Add(newElementType);
                    _context.Add(newElementType);
                }

                foreach (RPGElementDefinition elementDefinition in existing.ElementDefinitions)
                {
                    if(rPGSystem.ElementDefinitions.Select(x=>x.Id).Contains(elementDefinition.Id))
                    {
                        _context.Entry(elementDefinition).CurrentValues.SetValues(rPGSystem.ElementDefinitions.First(x => x.Id == elementDefinition.Id));
                    }
                    else
                    {
                        rPGSystem.ElementDefinitions.Remove(elementDefinition);
                        _context.Remove(elementDefinition);
                    }
                }
                foreach (RPGElementDefinition newElementDefinition in rPGSystem.ElementDefinitions.Where(x=>x.Id==0))
                {
                    newElementDefinition.ElementType = existing.ElementTypes.First(x => x.TypeName == newElementDefinition.ElementType.TypeName);
                    existing.ElementDefinitions.Add(newElementDefinition);
                    _context.Add(newElementDefinition);
                }
            }
        }
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        _context.RPGSystems.Remove(new RPGSystem { Id = id });
        await _context.SaveChangesAsync();
    }

    private static IEnumerable<RPGSystemHeadingDto> ConvertToHeadings(List<RPGSystem> systems)
    {
        var systemDtos = new List<RPGSystemHeadingDto>();
        foreach (RPGSystem? system in systems)
        {
            systemDtos.Add(system.ToHeadingDto());
        }
        return systemDtos;
    }
}
