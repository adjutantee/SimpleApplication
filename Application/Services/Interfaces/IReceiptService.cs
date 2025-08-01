using Application.DTO;

namespace Application.Services.Interfaces
{
    public interface IReceiptService
    {
        Task<ReceiptDto> CreateReceiptAsync(ReceiptWithItemsDto dto);
        Task<bool> DeleteReceiptAsync(int id);
        Task<List<ReceiptDto>> GetAllReceiptsAsync();
        Task<ReceiptWithItemsDto> GetReceiptByIdAsync(int id);
        Task<ReceiptDto> UpdateReceiptAsync(ReceiptWithItemsDto dto);
        Task<List<ReceiptDto>> GetFilteredReceiptsAsync(ReceiptFilterDto filter);
    }
}
