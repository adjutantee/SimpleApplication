using Application.DTO;
using Domain.Interfaces;

namespace Infrastructure.Services
{
    public class ResourceService : IResourceService
    {
        public Task<ResourceDto> AddResourceAsync(ResourceDto resourceDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteResourceAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ResourceDto>> GetAllResourcesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResourceDto> GetResourceByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResourceDto> UpdateResourceByAsync(ResourceDto resourceDto)
        {
            throw new NotImplementedException();
        }
    }
}
