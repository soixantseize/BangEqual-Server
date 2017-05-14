using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BareMetalApi.Models;
using BareMetalApi.Repositories.Interfaces;

namespace BareMetalApi.Repositories
{
    public class ShopDesignRepository : IShopDesignRepository
    {
        private readonly ApplicationDbContext _context;

        public ShopDesignRepository(ApplicationDbContext context)
        {
            _context = context;    
        }

        public async Task<bool> DoesItemExist(int id)
        {
            var dbEntity = await _context.ShopDesigns.AnyAsync(shopdesign => shopdesign.DesignId == id);
            return dbEntity;
        }
        public async Task <IList<ShopDesign>> GetAll()
        {
            var dbEntity = await _context.ShopDesigns.ToListAsync();
            return dbEntity;
        }

        public async Task <ShopDesign> GetById(int id)
        {
            var dbEntity = await _context.ShopDesigns
				.SingleOrDefaultAsync(m => m.DesignId == id);
            return dbEntity;
            
        }

        public async Task <int> AddAsync(ShopDesign shopdesign)
        {
            _context.ShopDesigns.Add(new ShopDesign { Title = shopdesign.Title, Content = shopdesign.Content});
            return await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ShopDesign shopdesign)
        {
            _context.Attach(shopdesign);
            var entry = _context.Entry(shopdesign);
            entry.Property(e => e.Title).IsModified = true;
            entry.Property(e => e.Content).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ShopDesign shopdesign)
        {
            _context.ShopDesigns.Remove(shopdesign);
            await _context.SaveChangesAsync();
        }
    }
}