using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Triarch.Repositories;
using Triarch.Dtos.Definitions;

namespace Triarch.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoreRulesetsController : ControllerBase
{
    private readonly ICoreRuleSetRepository _coreRuleSetRepository;

    public CoreRulesetsController(ICoreRuleSetRepository coreRuleSetRepository)
    {
        _coreRuleSetRepository = coreRuleSetRepository;
    }

    // GET: api/CoreRulesets
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CoreRulesetDto>>> GetCoreRulesets()
    {
        return Ok(await _coreRuleSetRepository.GetAllAsync());
        //if (_context.CoreRulesets == null)
        //{
        //    return NotFound();
        //} 

        //return await _context.CoreRulesets.Select(x => new CoreRulesetDto { Id = x.Id, CoreRulesetName = x.CoreRulesetName }).ToListAsync();
    }

    // GET: api/CoreRulesets/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CoreRulesetDto>> GetCoreRuleset(int id)
    {
        //if (_context.CoreRulesets == null)
        //{
            return NotFound();
        //}
        //var coreRuleset = await _context.CoreRulesets.FindAsync(id);

        //if (coreRuleset == null)
        //{
        //    return NotFound();
        //}

        //return new CoreRulesetDto
        //{
        //    Id = coreRuleset.Id,
        //    CoreRulesetName = coreRuleset.CoreRulesetName
        //};
    }

    // PUT: api/CoreRulesets/5
    [HttpPut]
    public async Task<IActionResult> PutCoreRuleset(CoreRulesetDto coreRulesetDTO)
    {
        //CoreRuleset coreRuleset = new CoreRuleset 
        //{
        //    Id = coreRulesetDTO.Id,
        //    CoreRulesetName= coreRulesetDTO.CoreRulesetName
        //};

        //_context.Entry(coreRuleset).State = EntityState.Modified;

        //try
        //{
        //    await _context.SaveChangesAsync();
        //}
        //catch (DbUpdateConcurrencyException)
        //{
        //    if (!CoreRulesetExists(coreRulesetDTO.Id))
        //    {
                return NotFound();
        //    }
        //    else
        //    {
        //        throw;
        //    }
        //}

        //return NoContent();
    }

    // POST: api/CoreRulesets
    [HttpPost]
    public async Task<ActionResult<CoreRulesetDto>> PostCoreRuleset(CoreRulesetDto coreRulesetDTO)
    {
        return NotFound();
      //if (_context.CoreRulesets == null)
      //{
      //    return Problem("Entity set 'TriarchDbContext.CoreRulesets' is null.");
      //}
      //  CoreRuleset coreRuleset = new CoreRuleset 
      //  {
      //      Id=0,
      //      CoreRulesetName=coreRulesetDTO.CoreRulesetName
      //  };
      //  _context.CoreRulesets.Add(coreRuleset);
      //  await _context.SaveChangesAsync();

      //  coreRulesetDTO.Id = coreRuleset.Id;

      //  return CreatedAtAction("GetCoreRuleset", new { id = coreRuleset.Id }, coreRulesetDTO);
    }

    // DELETE: api/CoreRulesets/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCoreRuleset(int id)
    {
        return NotFound();
        //if (_context.CoreRulesets == null)
        //{
        //    return NotFound();
        //}
        //var coreRuleset = await _context.CoreRulesets.FindAsync(id);
        //if (coreRuleset == null)
        //{
        //    return NotFound();
        //}

        //_context.CoreRulesets.Remove(coreRuleset);
        //await _context.SaveChangesAsync();

        //return NoContent();
    }

    private bool CoreRulesetExists(int id)
    {
        return false;
        //return (_context.CoreRulesets?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
