using Domain.Models;
using Domain.Repositories.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ReceiptRepository : IReceiptRepository
    {
        private readonly ApplicationDbContext _context;

        public ReceiptRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Receipt> AddAsync(Receipt receipt)
        {
            if (await _context.Receipts.AnyAsync(r => r.Number == receipt.Number))
            {
                throw new InvalidOperationException("Документ с таким номером уже существует");
            }

            _context.Receipts.Add(receipt);
            await _context.SaveChangesAsync();
            return receipt;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var receipt = await _context.Receipts.FindAsync(id);
            if (receipt == null) return false;

            _context.Receipts.Remove(receipt);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Receipt> GetByIdAsync(int id)
        {
            return await _context.Receipts.FindAsync(id);
        }

        public async Task<Receipt?> GetByIdWithItemsAsync(int id)
        {
            return await _context.Receipts
                .Include(r => r.ReceiptItems)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<Receipt>> GetAllWithItemsAsync()
        {
            return await _context.Receipts
                .Include(r => r.ReceiptItems)
                .ToListAsync();
        }

        public async Task<Receipt> UpdateAsync(Receipt receipt)
        {
            _context.Receipts.Update(receipt);
            await _context.SaveChangesAsync();
            return receipt;
        }

        public async Task<List<Receipt>> GetFilteredAsync(
            DateTime? startDate,
            DateTime? endDate,
            List<string> documentNumbers,
            List<int> resourceIds,
            List<int> unitIds)
        {
            var query = _context.Receipts
                .Include(r => r.ReceiptItems)
                .AsQueryable();

            if (startDate.HasValue)
                query = query.Where(r => r.Date >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(r => r.Date <= endDate.Value);

            if (documentNumbers != null && documentNumbers.Any())
                query = query.Where(r => documentNumbers.Contains(r.Number));

            if (resourceIds != null && resourceIds.Any())
                query = query.Where(r => r.ReceiptItems.Any(i => resourceIds.Contains(i.ResourceId)));

            if (unitIds != null && unitIds.Any())
                query = query.Where(r => r.ReceiptItems.Any(i => unitIds.Contains(i.UnitId)));

            return await query.ToListAsync();
        }
    }
}
