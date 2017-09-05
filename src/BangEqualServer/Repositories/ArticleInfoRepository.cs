using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BangEqualServer.Models;
using BangEqualServer.Repositories.Interfaces;

namespace BangEqualServer.Repositories
{
    public class ArticleInfoRepository : IArticleInfoRepository
    {
        private readonly ApplicationDbContext _context;

        public ArticleInfoRepository(ApplicationDbContext context)
        {
            _context = context;    
        }

        public async Task<bool> DoesItemExist(int id)
        {
            //var dbEntity = await _context.SiteContent.AnyAsync(content => content.ContentId == id);
            //return dbEntity;
            return true;
        }
        public async Task <IList<IList<ArticleInfo>>> GetArticle(string type, int chunksize)
        {
			
            //var dbEntity = await _context.SiteContent
                //.Where(c => c.Type == type)
                //.Take(chunksize * 2)
                //.ToListAsync();

            //https://codereview.stackexchange.com/questions/90195/generic-method-to-split-provided-collection-into-smaller-collections

            //var chunks = new List<IList<ArticleInfo>>();

            //var chunkCount = dbEntity.Count() / chunksize;

            //If remainder is greater than 0, then add to chunkCount
            //if (dbEntity.Count % chunksize > 0)
                //chunkCount++;
            
            //for (var i = 0; i < chunkCount; i++)
            //{
                //chunks.Add(dbEntity.Skip(i * chunksize).Take(chunksize).ToList());
            //}
            
            //return chunks;
            return null;
        }

        public async Task <IList<IList<ArticleInfo>>> GetByTopicAndType(string topic, int chunksize, string type)
        {
            //var dbEntity = await _context.SiteContent
               // .Where(c => c.Topic == topic && c.Type == type)
				//.Take(chunksize * 2)
                //.ToListAsync();

            //var chunks = new List<IList<Content>>();

            //var chunkCount = dbEntity.Count() / chunksize;

            //If remainder is greater than 0, then add to chunkCount
            //if (dbEntity.Count % chunksize > 0)
                //chunkCount++;
            
            //for (var i = 0; i < chunkCount; i++)
            //{
               // chunks.Add(dbEntity.Skip(i * chunksize).Take(chunksize).ToList());
            //}
            
            //return chunks;
            return null;
            
        }

        public async Task <IList<string>> GetTopic(string type)
        {
            //var dbEntity = await _context.SiteContent
                //.Where(c => c.Type == type)
                //.Select(t => t.Topic) 
                //.Distinct()
                //.ToListAsync();

            //return dbEntity;
            return null;
        }

        public async Task <ArticleInfo> GetById(int id)
        {
            //var dbEntity = await _context.SiteContent
				//.SingleOrDefaultAsync(m => m.ContentId == id);
            //return dbEntity;
            return null;
            
        }

        public async Task <int> AddAsync(ArticleInfo content)
        {
            //_context.SiteContent.Add(new Content { Title = content.Title, RenderString = content.RenderString});
            //return await _context.SaveChangesAsync();
            return 0;
        }

        public async Task UpdateAsync(ArticleInfo c)
        {
            //_context.Attach(c);
            //var entry = _context.Entry(c);
            //entry.Property(e => e.Title).IsModified = true;
            //entry.Property(e => e.RenderString).IsModified = true;
            //await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ArticleInfo c)
        {
            //_context.SiteContent.Remove(c);
            //await _context.SaveChangesAsync();
        }
    }
}