using Application.DTO;

namespace Domain.Interfaces
{
    public interface IResourceService
    {
        Task<List<ResourceDto>> GetAllResourcesAsync();
        Task<ResourceDto> GetResourceByIdAsync(int id);
        Task<ResourceDto> AddResourceAsync(ResourceDto resourceDto);
        Task<ResourceDto> UpdateResourceByAsync(ResourceDto resourceDto);
        Task<bool> DeleteResourceAsync(int id);
    }
}
