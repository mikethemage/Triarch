using Triarch.Dtos.Definitions;

namespace Triarch.Repositories;
public interface IRPGSystemRepository
{
    Task DeleteAsync(string name, int userId);
    Task<IEnumerable<RPGSystemHeadingDto>> GetAllAsync();
    Task<IEnumerable<RPGSystemHeadingDto>> GetAllByUserIdAsync(int userId);
    Task<RPGSystemDto> GetByNameAsync(string name, int userId);
    Task<RPGSystemDto> SaveAsync(RPGSystemDto rPGSystemDto);
}