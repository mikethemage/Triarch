using Microsoft.EntityFrameworkCore;
using Triarch.Database;
using Triarch.Dtos.Definitions;
using Triarch.Database.Models.Definitions;
using Triarch.Repositories.Mappers;

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
        List<CoreRulesetDto> coreRulesetDtos = new List<CoreRulesetDto>();
        List<CoreRuleset> rulesets = await _context.CoreRulesets.ToListAsync();
        
        foreach (CoreRuleset ruleset in rulesets)
        {
            coreRulesetDtos.Add(ruleset.ToDto());
        }

        return coreRulesetDtos;
    }

    public async Task<CoreRulesetDto> GetByIdAsync(int id)
    {
        CoreRuleset? ruleset = await _context.CoreRulesets.Where(x=>x.Id==id).SingleOrDefaultAsync();
        if(ruleset == null)
        {
            throw new Exception("Core Rule Set not found");
        }

        return ruleset.ToDto();
    }

    public async Task<CoreRulesetDto> SaveAsync(CoreRulesetDto coreRuleSetDto)
    {
        if(coreRuleSetDto.Id!=0)
        {
            CoreRuleset? existing = await _context.CoreRulesets.Where(x => x.Id == coreRuleSetDto.Id).SingleOrDefaultAsync();
            if (existing != null)
            {
                existing.CoreRulesetName = coreRuleSetDto.CoreRulesetName;
                await _context.SaveChangesAsync();
                return existing.ToDto();
            }
        }

        CoreRuleset newRuleset = coreRuleSetDto.ToModel();
        _context.Add(newRuleset);
        await _context.SaveChangesAsync();
        return newRuleset.ToDto();
    }

    public async Task DeleteAsync(int id)
    {
        CoreRuleset? existing = await _context.CoreRulesets.Where(x => x.Id == id).SingleOrDefaultAsync();
        if (existing == null)
        {
            throw new Exception("Core Rule Set not found");
        }
        else
        {
            _context.Remove(existing);
            await _context.SaveChangesAsync();
        }
    }
}
