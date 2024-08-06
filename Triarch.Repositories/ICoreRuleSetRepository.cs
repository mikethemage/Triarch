using Triarch.Dtos.Definitions;

namespace Triarch.Repositories;
public interface ICoreRuleSetRepository
{
    Task DeleteAsync(string name);
    Task<IEnumerable<CoreRulesetDto>> GetAllAsync();
    Task<CoreRulesetDto> GetByNameAsync(string name);
    Task<CoreRulesetDto> SaveAsync(CoreRulesetDto coreRuleSetDto);
}