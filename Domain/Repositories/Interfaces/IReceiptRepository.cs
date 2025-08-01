using Domain.Models;

namespace Domain.Repositories.Interfaces
{
    public interface IReceiptRepository
    {
        Task<Receipt> AddAsync(Receipt receipt);
        Task<bool> DeleteAsync(int id);
        Task<Receipt> GetByIdAsync(int id);
        Task<Receipt> UpdateAsync(Receipt receipt);
        Task<List<Receipt>> GetAllWithItemsAsync();
        Task<Receipt?> GetByIdWithItemsAsync(int id);
        Task<List<Receipt>> GetFilteredAsync(
            DateTime? startDate,
            DateTime? endDate,
            List<string> documentNumbers,
            List<int> resourceIds,
            List<int> unitIds);
    }
}