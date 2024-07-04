using Microsoft.EntityFrameworkCore;
using Triarch.Database;
using Triarch.Dtos.Definitions;
using Triarch.Database.Models.Definitions;
using Triarch.Repositories.Mappers;
using Triarch.Repositories.Exceptions;


namespace Triarch.Repositories;
public class CoreRuleSetRepository : ICoreRuleSetRepository
{
    private readonly TriarchDbContext _context;

    public CoreRuleSetRepository(TriarchDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CoreRulesetDto>> GetAllAsync()
    {
        List<CoreRulesetDto> output = new List<CoreRulesetDto>();
        List<CoreRuleset> rulesets = await _context.CoreRulesets.ToListAsync();               

        foreach (CoreRuleset ruleset in rulesets)
        {
            output.Add(ruleset.ToDto());
        }

        return output;
    }

    public async Task<CoreRulesetDto> GetByIdAsync(int id)
    {
        CoreRuleset? ruleset = await _context.CoreRulesets.Where(x => x.Id == id).SingleOrDefaultAsync();
        if (ruleset == null)
        {
            throw new CoreRulesetNotFoundException($"Core Rule Set not found: {id}", id);
        }

        return ruleset.ToDto();
    }

    public async Task<CoreRulesetDto> SaveAsync(CoreRulesetDto input)
    {
        CoreRuleset? existing;

        if (input.Id != 0)
        {
            existing = await _context.CoreRulesets.Where(x => x.Id == input.Id).SingleOrDefaultAsync();
            if (existing != null)
            {
                if(existing.CoreRulesetName!=input.CoreRulesetName)
                {
                    //Rename existing Id
                    CoreRuleset? conflicting = await _context.CoreRulesets.Where(x => x.Id != existing.Id && x.CoreRulesetName == input.CoreRulesetName).FirstOrDefaultAsync();
                    if (conflicting != null)
                    {
                        //Errror - conflicting name found:
                        throw new CoreRulesetConflictException($"Error: cannot save ruleset id: {input.Id} name: {input.CoreRulesetName}.  Conflict with id: {conflicting.Id}, name: {conflicting.CoreRulesetName}.",
                                                               existing.Id,
                                                               existing.CoreRulesetName,
                                                               input.CoreRulesetName,
                                                               conflicting.Id);
                    }
                    existing.CoreRulesetName = input.CoreRulesetName;
                    await _context.SaveChangesAsync();                    
                }
                return existing.ToDto();
            }            
        }

        existing = await _context.CoreRulesets.Where(x => x.CoreRulesetName == input.CoreRulesetName).FirstOrDefaultAsync();
        if (existing != null)
        {
            return existing.ToDto();
        }
        else
        {
            //Add new
            var toAdd =input.ToModel();
            _context.Add(toAdd);
            await _context.SaveChangesAsync();
            return toAdd.ToDto();
        }        
    }

    public async Task DeleteAsync(int id)
    {
        CoreRuleset? existing = await _context.CoreRulesets.Where(x => x.Id == id).SingleOrDefaultAsync();
        if (existing == null)
        {
            throw new CoreRulesetNotFoundException($"Core Rule Set not found: {id}", id);
        }
        else
        {
            _context.Remove(existing);
            await _context.SaveChangesAsync();
        }
    }
}
