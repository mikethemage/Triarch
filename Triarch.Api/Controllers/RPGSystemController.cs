using Microsoft.AspNetCore.Mvc;
using Triarch.Repositories;
using Triarch.Dtos.Definitions;
using Triarch.Repositories.Exceptions;

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
    [ProducesResponseType<IEnumerable<RPGSystemHeadingDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<RPGSystemHeadingDto>>> GetRPGSystems()
    {
        return Ok(await _rPGSystemRepository.GetAllAsync());
    }

    // GET api/<RPGSystemController>/5
    [HttpGet("{id}")]
    [ProducesResponseType<RPGSystemHeadingDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RPGSystemDto>> Get(int id)
    {
        try
        {
            return Ok(await _rPGSystemRepository.GetByIdAsync(id));
        }
        catch (RPGSystemNotFoundException ex) 
        {
            return NotFound(ex.Message);
        }
    }

    // POST api/<RPGSystemController>
    [HttpPost]
    [ProducesResponseType<RPGSystemDto>(StatusCodes.Status201Created)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RPGSystemDto>> Post([FromBody] RPGSystemDto value)
    {
        try
        {
            RPGSystemDto output = await _rPGSystemRepository.SaveAsync(value);
            return CreatedAtAction(nameof(Get), new { id = output.Id }, output);
        }
        catch (RPGSystemConflictException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE api/<RPGSystemController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _rPGSystemRepository.DeleteAsync(id);
            return NoContent();
        }
        catch (RPGSystemNotFoundException ex)
        {
            return NotFound(ex.Message);  
        }
        
    }
}
