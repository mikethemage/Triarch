using Triarch.Dtos.Definitions;

namespace Triarch.Repositories;
public interface IRPGSystemRepository
{
    Task DeleteAsync(int id);
    Task<IEnumerable<RPGSystemHeadingDto>> GetAllAsync();
    Task<IEnumerable<RPGSystemHeadingDto>> GetAllByUserIdAsync(int userId);
    Task<RPGSystemDto> GetByIdAsync(int id);
    Task SaveAsync(RPGSystemDto rPGSystemDto);
}