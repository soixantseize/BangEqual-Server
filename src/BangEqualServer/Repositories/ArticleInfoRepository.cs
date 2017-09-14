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
            var dbEntity = await _context.ArticleInfo.AnyAsync(article => article.ArticleIdFK == id);
            return dbEntity;
        }
        public async Task <IList<IList<ArticleInfo>>> GetArticleInfo(int chunksize)
        {
			
            var dbEntity = await _context.ArticleInfo
                .Take(chunksize * 2)
                .ToListAsync();

            //https://codereview.stackexchange.com/questions/90195/generic-method-to-split-provided-collection-into-smaller-collections
            foreach(ArticleInfo ai in dbEntity)
            {
                ai.ArticleCaption = await this.GetArticleCaption(ai.ArticleIdFK);
            }

            var chunks = new List<IList<ArticleInfo>>();

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

        public async Task <IList<IList<ArticleInfo>>> GetArticleInfoByTag(string tag, int chunksize)
        {
            var dbEntity = await _context.ArticleInfo
                .Where(c => c.ArticleTags == tag)
				.Take(chunksize * 2)
                .ToListAsync();

            var chunks = new List<IList<ArticleInfo>>();

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

        public async Task <string> GetArticleCaption(int id)
        {
            var dbEntity = await _context.Article
            .SingleOrDefaultAsync(m => m.ArticleId == id);

            return dbEntity.ArticleCaption;
        }

        public async Task <string> GetArticleText(int id)
        {
            var dbEntity = await _context.Article
            .SingleOrDefaultAsync(m => m.ArticleId == id);

            return dbEntity.ArticleText;
        }
		
		public async Task <IList<string>> GetArticleInfoTags()
        {
            var dbEntity = await _context.ArticleInfo
                .Select(t => t.ArticleTags) 
                .Distinct()
                .ToListAsync();

            return dbEntity;
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