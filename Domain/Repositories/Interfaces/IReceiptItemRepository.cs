using Domain.Models;

namespace Domain.Repositories.Interfaces
{
    public interface IReceiptItemRepository
    {
        Task<ReceiptItem> AddAsync(ReceiptItem item);
        Task<List<ReceiptItem>> GetByReceiptIdAsync(int receiptId);
        Task DeleteByReceiptIdAsync(int receiptId);
    }
}
