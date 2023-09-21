using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DTOs;
using Triarch.RPGSystem.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RPGSystemController : ControllerBase
{
    private readonly TriarchDbContext _context;

    public RPGSystemController(TriarchDbContext context)
    {
        _context = context;
    }

    // GET: api/<RPGSystemController>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RPGSystemDTO>>> GetRPGSystems()
    {
        if (_context.RPGSystems == null)
        {
            return NotFound();
        }


        return await _context.RPGSystems.Select(x => new RPGSystemDTO { Id = x.Id, SystemName = x.SystemName, DescriptiveName = x.DescriptiveName, OwnerUserId = x.OwnerUserId, Ruleset = new CoreRulesetDTO { Id = x.Ruleset.Id, CoreRulesetName = x.Ruleset.CoreRulesetName } }).ToListAsync();
        
    }

    // GET api/<RPGSystemController>/5
    [HttpGet("{id}")]
    public RPGSystemDTO Get(int id)
    {
        return new RPGSystemDTO();
    }

    // POST api/<RPGSystemController>
    [HttpPost]
    public void Post([FromBody] RPGSystemDTO value)
    {
    }

    // PUT api/<RPGSystemController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] RPGSystemDTO value)
    {
    }

    // DELETE api/<RPGSystemController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
