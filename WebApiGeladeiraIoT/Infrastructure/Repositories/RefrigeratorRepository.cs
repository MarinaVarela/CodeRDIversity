using ApiRefrigerator.Models;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApiRefrigerator.Repository
{
    public class RefrigeratorRepository : IRefrigeratorRepository

    {
        private readonly RefrigeratorContext _context;

        public RefrigeratorRepository(RefrigeratorContext context)
        {
            _context = context;
        }

        public async Task<List<Refrigerator>> GetAllAsync()
        {
            var items = await _context.Refrigerator.ToListAsync();
            if (!items.Any())
                throw new InvalidDataException();

            return items;
        }

        public async Task<Refrigerator?> GetByIdAsync(int id)
        {
            var existItem = await _context.Refrigerator.FindAsync(id);
            if (existItem is null)
                return null;

            return existItem;
        }

        public async Task<Refrigerator?> GetByNameAsync(string name)
        {
            var searchItem = await _context.Refrigerator.FirstOrDefaultAsync(i => i.Name.Equals(name));
            if (searchItem is null)
                return null;
            return searchItem;
        }

        public async Task<Refrigerator?> InsertItemAsync(Refrigerator item)
        {
            try
            {
                _context.Refrigerator.Add(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Refrigerator?> UpdateItemAsync(Refrigerator item)
        {
            try
            {
                var existItem = await GetByIdAsync(item.Id);
                if (existItem is null)
                    return null;

                _context.Entry(existItem).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
                return existItem;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Refrigerator?> RemoveItemAsync(int id)
        {
            try
            {
                var existItem = await GetByIdAsync(id);
                if (existItem is null)
                    return null;

                _context.Refrigerator.Remove(existItem);
                await _context.SaveChangesAsync();

                return existItem;
            }
            catch
            {
                throw new ApplicationException("Unable to remove item.");
            }
        }

        public async Task<int> RemoveAllAsync()
        {
            try
            {
                var existItem = await GetAllAsync();
                if (existItem is not null)
                {
                    _context.Refrigerator.RemoveRange(existItem);
                    await _context.SaveChangesAsync();
                }
                else { return 0; }
                return existItem.Count;
            }
            catch
            {
                throw new ApplicationException("Unable to remove all items.");
            }
        }
    }
}
