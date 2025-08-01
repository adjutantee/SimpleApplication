using Domain.Enums;
using Domain.Models;
using Domain.Repositories.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly ApplicationDbContext _context;

        public ResourceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Resource> AddAsync(Resource resource)
        {
            await _context.Resources.AddAsync(resource);
            await _context.SaveChangesAsync();

            return resource;
        }

        public async Task<Resource> ArchiveAsync(int id)
        {
            var resource = await _context.Resources.FindAsync(id);

            switch (resource)
            {
                case null:
                    throw new Exception("Ресурс отсутствует в базе");

                case { State: State.Archived }:
                    resource.State = State.Active;
                    _context.Resources.Update(resource);
                    await _context.SaveChangesAsync();
                    return resource;

                case { State: State.Active }:
                    resource.State = State.Archived;
                    _context.Resources.Update(resource);
                    await _context.SaveChangesAsync();

                    return resource;

                default:
                    throw new Exception($"Неизвестное состояние ресурса: {resource.State}");
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var resource = await _context.Resources.FindAsync(id);

            if (resource == null)
            {
                return false;
            }

            _context.Resources.Remove(resource);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Resource>> GetAllAsync()
        {
            return await _context.Resources
                .Where(x => x.State == State.Active)
                .ToListAsync();
        }

        public async Task<List<Resource>> GetAllArchiveAsync()
        {
            return await _context.Resources
                .Where(x => x.State == State.Archived)
                .ToListAsync();
        }

        public async Task<Resource> GetByIdAsync(int id)
        {
            var resource = await _context.Resources.FindAsync(id);

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

        public async Task<Resource> UpdateAsync(Resource resource)
        {
            _context.Resources.Update(resource);
            await _context.SaveChangesAsync();
            return resource;
        }
    }
}
