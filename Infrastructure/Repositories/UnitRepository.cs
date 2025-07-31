using Domain.Enums;
using Domain.Models;
using Domain.Repositories.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UnitRepository : IUnitRepository
    {
        private readonly ApplicationDbContext _context;

        public UnitRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> AddAsync(Unit unit)
        {
            await _context.Unit.AddAsync(unit);
            await _context.SaveChangesAsync();

            return unit;
        }

        public async Task<Unit> ArchiveAsync(int id)
        {
            var unit = await _context.Unit.FindAsync(id);

            switch (unit)
            {
                case null:
                    throw new Exception("Единица измерения отсутствует в базе");

                case { State: State.Archived }:
                    unit.State = State.Active;
                    _context.Unit.Update(unit);
                    await _context.SaveChangesAsync();
                    return unit;

                case { State: State.Active }:
                    unit.State = State.Archived;
                    _context.Unit.Update(unit);
                    await _context.SaveChangesAsync();

                    return unit;

                default:
                    throw new Exception($"Неизвестное состояние единицы измерения: {unit.State}");
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var unit = await _context.Unit.FindAsync(id);

            if (unit == null)
            {
                return false;
            }

            _context.Unit.Remove(unit);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Unit>> GetAllArchiveAsync()
        {
            return await _context.Unit
                .Where(x =>  x.State == State.Archived)
                .ToListAsync();
        }

        public async Task<List<Unit>> GetAllAsync()
        {
            return await _context.Unit
                .Where(x => x.State == State.Active)
                .ToListAsync();
        }

        public async Task<Unit> GetByIdAsync(int id)
        {
            var resource = await _context.Unit.FindAsync(id);

            if (resource == null)
            {
                throw new Exception("Ресурс отсутствует в базе");
            }

            return resource;
        }

        public async Task<bool> IsInUseAsync(int id)
        {
            return await _context.ReceiptsItem.AnyAsync(ri => ri.ResourceId == id);
        }

        public async Task<Unit> UpdateAsync(Unit unit)
        {
            _context.Unit.Update(unit);
            await _context.SaveChangesAsync();

            return unit;
        }
    }
}
