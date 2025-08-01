using Application.DTO;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Models;
using Domain.Repositories.Interfaces;

namespace Application.Services
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _unitRepository;
        private IMapper _mapper;

        public UnitService(IUnitRepository unitRepository, IMapper mapper)
        {
            _unitRepository = unitRepository;
            _mapper = mapper;
        }

        public async Task<UnitDto> AddUnitAsync(UnitDto unitDto)
        {
            var unit = _mapper.Map<Unit>(unitDto);
            var createdUnit = await _unitRepository.AddAsync(unit);

            return _mapper.Map<UnitDto>(createdUnit);

        }

        public async Task<UnitDto> ArchiveAsync(int id)
        {
            var unit = await _unitRepository.ArchiveAsync(id);

            return _mapper.Map<UnitDto>(unit);
        }

        public async Task<bool> DeleteUnitAsync(int id)
        {
            bool isDeleted = await _unitRepository.DeleteAsync(id);

            return isDeleted;
        }

        public async Task<List<UnitDto>> GetAllArchiveAsync()
        {
            var unit = await _unitRepository.GetAllArchiveAsync();

            return _mapper.Map<List<UnitDto>>(unit);
        }

        public async Task<List<UnitDto>> GetAllUnitsAsync()
        {
            var unit = await _unitRepository.GetAllAsync();

            return _mapper.Map<List<UnitDto>>(unit);
        }

        public async Task<UnitDto> GetUnitByIdAsync(int id)
        {
            var unit = await _unitRepository.GetByIdAsync(id);

            return _mapper.Map<UnitDto>(unit);
        }

        public async Task<UnitDto> UpdateUnitByAsync(UnitDto unitDto)
        {
            var unit = _mapper.Map<Unit>(unitDto);
            var updatedUnit = await _unitRepository.UpdateAsync(unit);

            return _mapper.Map<UnitDto>(updatedUnit);
        }
    }
}
