using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Triarch.RPGSystem.Models;
using WebApplication1.DTOs;


namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoreRulesetsController : ControllerBase
{
    private readonly TriarchDbContext _context;

    public CoreRulesetsController(TriarchDbContext context)
    {
        _context = context;
    }

    // GET: api/CoreRulesets
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CoreRulesetDTO>>> GetCoreRulesets()
    {
      if (_context.CoreRulesets == null)
      {
          return NotFound();
      } 

      return await _context.CoreRulesets.Select(x => new CoreRulesetDTO { Id = x.Id, CoreRulesetName = x.CoreRulesetName }).ToListAsync();
    }

    // GET: api/CoreRulesets/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CoreRulesetDTO>> GetCoreRuleset(int id)
    {
        if (_context.CoreRulesets == null)
        {
            return NotFound();
        }
        var coreRuleset = await _context.CoreRulesets.FindAsync(id);

        if (coreRuleset == null)
        {
            return NotFound();
        }

        return new CoreRulesetDTO
        {
            Id = coreRuleset.Id,
            CoreRulesetName = coreRuleset.CoreRulesetName
        };
    }

    // PUT: api/CoreRulesets/5
    [HttpPut]
    public async Task<IActionResult> PutCoreRuleset(CoreRulesetDTO coreRulesetDTO)
    {
        CoreRuleset coreRuleset = new CoreRuleset 
        {
            Id = coreRulesetDTO.Id,
            CoreRulesetName= coreRulesetDTO.CoreRulesetName
        };

        _context.Entry(coreRuleset).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CoreRulesetExists(coreRulesetDTO.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/CoreRulesets
    [HttpPost]
    public async Task<ActionResult<CoreRulesetDTO>> PostCoreRuleset(CoreRulesetDTO coreRulesetDTO)
    {
      if (_context.CoreRulesets == null)
      {
          return Problem("Entity set 'TriarchDbContext.CoreRulesets' is null.");
      }
        CoreRuleset coreRuleset = new CoreRuleset 
        {
            Id=0,
            CoreRulesetName=coreRulesetDTO.CoreRulesetName
        };
        _context.CoreRulesets.Add(coreRuleset);
        await _context.SaveChangesAsync();

        coreRulesetDTO.Id = coreRuleset.Id;

        return CreatedAtAction("GetCoreRuleset", new { id = coreRuleset.Id }, coreRulesetDTO);
    }

    // DELETE: api/CoreRulesets/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCoreRuleset(int id)
    {
        if (_context.CoreRulesets == null)
        {
            return NotFound();
        }
        var coreRuleset = await _context.CoreRulesets.FindAsync(id);
        if (coreRuleset == null)
        {
            return NotFound();
        }

        _context.CoreRulesets.Remove(coreRuleset);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CoreRulesetExists(int id)
    {
        return (_context.CoreRulesets?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
