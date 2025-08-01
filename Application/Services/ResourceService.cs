using Application.DTO;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Models;
using Domain.Repositories.Interfaces;

namespace Application.Services
{
    public class ResourceService : IResourceService
    {
        private readonly IResourceRepository _resourceRepository;
        private IMapper _mapper;

        public ResourceService(IResourceRepository resourceRepository, IMapper mapper)
        {
            _resourceRepository = resourceRepository;
            _mapper = mapper;
        }
        public async Task<ResourceDto> AddResourceAsync(ResourceDto resourceDto)
        {
            var resource = _mapper.Map<Resource>(resourceDto);
            var createdResource = await _resourceRepository.AddAsync(resource);

            return _mapper.Map<ResourceDto>(createdResource);
        }

        public async Task<ResourceDto> ArchiveResource(int id)
        {
            var resource = await _resourceRepository.ArchiveAsync(id);

            return _mapper.Map<ResourceDto>(resource);
        }

        public async Task<bool> DeleteResourceAsync(int id)
        {
            bool isDeleted = await _resourceRepository.DeleteAsync(id);

            return isDeleted;
        }

        public async Task<List<ResourceDto>> GetAllArchiveResources()
        {
            var resource = await _resourceRepository.GetAllArchiveAsync();

            return _mapper.Map<List<ResourceDto>>(resource);
        }

        public async Task<List<ResourceDto>> GetAllResourcesAsync()
        {
            var resource = await _resourceRepository.GetAllAsync();

            return _mapper.Map<List<ResourceDto>>(resource);
        }

        public async Task<ResourceDto> GetResourceByIdAsync(int id)
        {
            var resource = await _resourceRepository.GetByIdAsync(id);

            return _mapper.Map<ResourceDto>(resource);
        }

        public async Task<ResourceDto> UpdateResourceByAsync(ResourceDto resourceDto)
        {
            var resource = _mapper.Map<Resource>(resourceDto);
            var updatedResource = await _resourceRepository.UpdateAsync(resource);

            return _mapper.Map<ResourceDto>(updatedResource);
        }
    }
}
