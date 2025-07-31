using Domain.Models;

namespace Domain.Repositories.Interfaces
{
    public interface IUnitRepository
    {
        Task<List<Unit>> GetAllAsync();
        Task<Unit> GetByIdAsync(int id);
        Task<Unit> AddAsync(Unit unit);
        Task<Unit> UpdateAsync(Unit unit);
        Task<bool> IsInUseAsync(int id);
        Task<bool> DeleteAsync(int id);

        // Логика работы архива
        Task<List<Unit>> GetAllArchiveAsync();
        Task<Unit> ArchiveAsync(int id);
    }
}
