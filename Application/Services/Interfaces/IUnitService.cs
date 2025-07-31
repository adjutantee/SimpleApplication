using Application.DTO;

namespace Application.Services.Interfaces
{
    public interface IUnitService
    {
        Task<List<UnitDto>> GetAllUnitsAsync();
        Task<UnitDto> GetUnitByIdAsync(int id);
        Task<UnitDto> AddUnitAsync(UnitDto unitDto);
        Task<UnitDto> UpdateUnitByAsync(UnitDto unitDto);
        Task<bool> DeleteUnitAsync(int id);
    }
}
