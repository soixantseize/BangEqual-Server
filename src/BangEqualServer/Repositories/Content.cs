using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BareMetalApi.Models;
using BareMetalApi.Repositories.Interfaces;

namespace BareMetalApi.Repositories
{
    public class ContentRepository : IContentRepository
    {
        private readonly ApplicationDbContext _context;

        public ContentRepository(ApplicationDbContext context)
        {
            _context = context;    
        }

        public async Task<bool> DoesItemExist(int id)
        {
            var dbEntity = await _context.SiteContent.AnyAsync(content => content.ContentId == id);
            return dbEntity;
        }
        public async Task <IList<Content>> GetContent(string type)
        {
            var dbEntity = await _context.SiteContent
                .Where(c => c.Type == type)
                .ToListAsync();
            
            return dbEntity;
        }

        public async Task <Content> GetById(int id)
        {
            var dbEntity = await _context.SiteContent
				.SingleOrDefaultAsync(m => m.ContentId == id);
            return dbEntity;
            
        }

        public async Task <int> AddAsync(Content content)
        {
            _context.SiteContent.Add(new Content { Title = content.Title, RenderString = content.RenderString});
            return await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Content c)
        {
            _context.Attach(c);
            var entry = _context.Entry(c);
            entry.Property(e => e.Title).IsModified = true;
            entry.Property(e => e.RenderString).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Content c)
        {
            _context.SiteContent.Remove(c);
            await _context.SaveChangesAsync();
        }
    }
}