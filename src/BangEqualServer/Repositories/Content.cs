using System;
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
        public async Task <IList<IList<Content>>> GetContent(string type, int chunksize)
        {
			
            var dbEntity = await _context.SiteContent
                .Where(c => c.Type == type)
                .Take(chunksize * 2)
                .ToListAsync();

            //https://codereview.stackexchange.com/questions/90195/generic-method-to-split-provided-collection-into-smaller-collections

            var chunks = new List<IList<Content>>();

            var chunkCount = dbEntity.Count() / chunksize;

            //If remainder is greater than 0, then add to chunkCount
            if (dbEntity.Count % chunksize > 0)
                chunkCount++;
            
            for (var i = 0; i < chunkCount; i++)
            {
                chunks.Add(dbEntity.Skip(i * chunksize).Take(chunksize).ToList());
            }
            
            return chunks;
        }

        public async Task <IList<IList<Content>>> GetByTopic(string topic, int chunksize, string type)
        {
            var dbEntity = await _context.SiteContent
                .Where(c => c.Topic == topic && c.Type == type)
				.Take(chunksize * 2)
                .ToListAsync();

            var chunks = new List<IList<Content>>();

            var chunkCount = dbEntity.Count() / chunksize;

            //If remainder is greater than 0, then add to chunkCount
            if (dbEntity.Count % chunksize > 0)
                chunkCount++;
            
            for (var i = 0; i < chunkCount; i++)
            {
                chunks.Add(dbEntity.Skip(i * chunksize).Take(chunksize).ToList());
            }
            
            return chunks;
            
        }

        public async Task <IList<string>> GetAllTopic(string type)
        {
            var dbEntity = await _context.SiteContent
                .Where(c => c.Type == type)
                .Select(t => t.Topic) 
                .Distinct()
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