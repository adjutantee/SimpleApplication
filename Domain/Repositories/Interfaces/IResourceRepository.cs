using Domain.Models;

namespace Domain.Repositories.Interfaces
{
    public interface IResourceRepository
    {
        Task<List<Resource>> GetAllAsync();
        Task<Resource> GetByIdAsync(int id);
        Task<Resource> AddAsync(Resource resource);
        Task<Resource> UpdateAsync(Resource resource);
        Task<bool> DeleteAsync(int id);
        Task<bool> IsInUseAsync(int id);

        // Логика работы архива
        Task<List<Resource>> GetAllArchiveAsync();
        Task<Resource> ArchiveAsync(int id);
    }
}
