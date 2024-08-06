using Microsoft.EntityFrameworkCore;
using Triarch.Database;
using Triarch.Dtos.Definitions;
using Triarch.Database.Models.Definitions;
using Triarch.Repositories.Mappers;
using Triarch.Repositories.Exceptions;
using System.Xml.Linq;


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

    public async Task<CoreRulesetDto> GetByNameAsync(string name)
    {
        CoreRuleset? ruleset = await _context.CoreRulesets.Where(x => x.CoreRulesetName == name).SingleOrDefaultAsync();
        if (ruleset == null)
        {
            throw new CoreRulesetNotFoundException($"Core Rule Set not found: {name}", name);
        }

        return ruleset.ToDto();
    }

    public async Task<CoreRulesetDto> SaveAsync(CoreRulesetDto input)
    {
        CoreRuleset? existing = await _context.CoreRulesets.Where(x => x.CoreRulesetName == input.CoreRulesetName).FirstOrDefaultAsync();
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

    public async Task DeleteAsync(string name)
    {
        CoreRuleset? existing = await _context.CoreRulesets.Where(x => x.CoreRulesetName == name).SingleOrDefaultAsync();
        if (existing == null)
        {
            throw new CoreRulesetNotFoundException($"Core Rule Set not found: {name}", name);
        }
        else
        {
            _context.Remove(existing);
            await _context.SaveChangesAsync();
        }
    }
}
