using Microsoft.AspNetCore.Mvc;
using Triarch.Dtos.Definitions;
using Triarch.Repositories;
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
    [HttpGet("{name}")]
    [ProducesResponseType<RPGSystemHeadingDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RPGSystemDto>> Get(string name)
    {
        try
        {
            return Ok(await _rPGSystemRepository.GetByNameAsync(name, 1));
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
            return CreatedAtAction(nameof(Get), new { name = output.SystemName }, output);
        }
        catch (RPGSystemConflictException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE api/<RPGSystemController>/5
    [HttpDelete("{name}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(string name)
    {
        try
        {
            await _rPGSystemRepository.DeleteAsync(name, 1);
            return NoContent();
        }
        catch (RPGSystemNotFoundException ex)
        {
            return NotFound(ex.Message);
        }

    }
}
