using Application.DTO;
using Domain.Interfaces;

namespace Infrastructure.Services
{
    public class UnitService : IUnitService
    {
        public Task<UnitDto> AddUnitAsync(UnitDto unitDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUnitAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<UnitDto>> GetAllUnitsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UnitDto> GetUnitByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UnitDto> UpdateUnitByAsync(UnitDto unitDto)
        {
            throw new NotImplementedException();
        }
    }
}
