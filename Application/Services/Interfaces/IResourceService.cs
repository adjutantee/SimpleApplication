using Application.DTO;

namespace Application.Services.Interfaces
{
    public interface IResourceService
    {
        Task<List<ResourceDto>> GetAllResourcesAsync();
        Task<ResourceDto> GetResourceByIdAsync(int id);
        Task<ResourceDto> AddResourceAsync(ResourceDto resourceDto);
        Task<ResourceDto> UpdateResourceByAsync(ResourceDto resourceDto);
        Task<bool> DeleteResourceAsync(int id);

        // Логика работы архива
        Task<List<ResourceDto>> GetAllArchiveResources();
        // Архивирование сущности
        Task<ResourceDto> ArchiveResource(int id);
    }
}
