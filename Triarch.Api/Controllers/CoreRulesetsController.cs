using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Triarch.Repositories;
using Triarch.Dtos.Definitions;
using Triarch.Repositories.Exceptions;

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
    [ProducesResponseType<IEnumerable<CoreRulesetDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CoreRulesetDto>>> GetCoreRulesets()
    {
        return Ok(await _coreRuleSetRepository.GetAllAsync());        
    }

    // GET: api/CoreRulesets/5
    [HttpGet("{name}")]
    [ProducesResponseType<CoreRulesetDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CoreRulesetDto>> GetCoreRuleset(string name)
    {
        try
        {
            return Ok(await _coreRuleSetRepository.GetByNameAsync(name));
        }
        catch (CoreRulesetNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }    

    // POST: api/CoreRulesets
    [HttpPost]
    [ProducesResponseType<CoreRulesetDto>(StatusCodes.Status201Created)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CoreRulesetDto>> PostCoreRuleset(CoreRulesetDto coreRulesetDTO)
    {
        try
        {
            CoreRulesetDto output = await _coreRuleSetRepository.SaveAsync(coreRulesetDTO);
            return CreatedAtAction(nameof(GetCoreRuleset), new { name = output.CoreRulesetName },output);
        }
        catch (CoreRulesetConflictException ex)
        {
            return BadRequest(ex.Message);
        }       
    }

    // DELETE: api/CoreRulesets/5
    [HttpDelete("{name}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteCoreRuleset(string name)
    {
        try
        {
            await _coreRuleSetRepository.DeleteAsync(name);
            return NoContent();
        }
        catch (CoreRulesetNotFoundException ex)
        {
            return NotFound(ex.Message);
        }       
    }    
}
