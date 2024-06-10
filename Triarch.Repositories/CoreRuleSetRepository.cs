using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triarch.Database;
using Triarch.Dtos.Definitions;

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
        return new List<CoreRulesetDto>();
    }

    public async Task<CoreRulesetDto> GetByIdAsync(int id)
    {
        if (_context.CoreRulesets == null)
        {
            throw new Exception("No Core Rule Sets found");
        }

        return await _context.CoreRulesets.Select(x => new CoreRulesetDto { Id = x.Id, CoreRulesetName = x.CoreRulesetName }).SingleAsync();

    }

    public async Task SaveAsync(CoreRulesetDto coreRuleSetDto)
    {

    }

    public async Task DeleteAsync(int id)
    {

    }
}
