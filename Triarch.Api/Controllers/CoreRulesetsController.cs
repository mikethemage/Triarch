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
    }

    // GET: api/CoreRulesets/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CoreRulesetDto>> GetCoreRuleset(int id)
    {
        try
        {
            return Ok(await _coreRuleSetRepository.GetByIdAsync(id));
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }    

    // POST: api/CoreRulesets
    [HttpPost]
    public async Task<ActionResult<CoreRulesetDto>> PostCoreRuleset(CoreRulesetDto coreRulesetDTO)
    {
        try
        {
            return Ok(await _coreRuleSetRepository.SaveAsync(coreRulesetDTO));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }       
    }

    // DELETE: api/CoreRulesets/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCoreRuleset(int id)
    {
        try
        {
            await _coreRuleSetRepository.DeleteAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }       
    }    
}
