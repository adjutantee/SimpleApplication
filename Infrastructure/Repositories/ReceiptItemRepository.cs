using Domain.Models;
using Domain.Repositories.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ReceiptItemRepository : IReceiptItemRepository
    {
        private readonly ApplicationDbContext _context;

        public ReceiptItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ReceiptItem> AddAsync(ReceiptItem item)
        {
            _context.ReceiptsItem.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<List<ReceiptItem>> GetByReceiptIdAsync(int receiptId)
        {
            return await _context.ReceiptsItem
                .Where(ri => ri.ReceiptId == receiptId)
                .ToListAsync();
        }

        public async Task DeleteByReceiptIdAsync(int receiptId)
        {
            var items = await _context.ReceiptsItem
                .Where(ri => ri.ReceiptId == receiptId)
                .ToListAsync();

            _context.ReceiptsItem.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
