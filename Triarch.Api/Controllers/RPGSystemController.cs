using Microsoft.AspNetCore.Mvc;
using Triarch.Repositories;
using Triarch.Dtos.Ruleset;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Triarch.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RPGSystemController : ControllerBase
{
    private readonly IRPGSystemRepository _rPGSystemRepository;

    public RPGSystemController(IRPGSystemRepository rPGSystemRepository)
    {
        _rPGSystemRepository = rPGSystemRepository;
    }

    // GET: api/<RPGSystemController>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RPGSystemDto>>> GetRPGSystems()
    {
        try
        {
            return Ok(await _rPGSystemRepository.GetAllAsync());
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
        
    }

    // GET api/<RPGSystemController>/5
    [HttpGet("{id}")]
    public async Task<RPGSystemDto> Get(int id)
    {
        return await _rPGSystemRepository.GetByIdAsync(id);
    }

    // POST api/<RPGSystemController>
    [HttpPost]
    public async Task Post([FromBody] RPGSystemDto value)
    {
        await _rPGSystemRepository.SaveAsync(value);
    }
    

    // DELETE api/<RPGSystemController>/5
    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {

    }
}
