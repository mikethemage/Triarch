using Triarch.Dtos.Ruleset;

namespace Triarch.Repositories;
public interface ICoreRuleSetRepository
{
    Task DeleteAsync(int id);
    Task<IEnumerable<CoreRulesetDto>> GetAllAsync();
    Task<CoreRulesetDto> GetByIdAsync(int id);
    Task SaveAsync(CoreRulesetDto coreRuleSetDto);
}