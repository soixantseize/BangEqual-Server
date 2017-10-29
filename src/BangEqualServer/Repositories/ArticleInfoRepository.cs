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
            //First request for articles will always be newest
            var dbEntity = await _context.ArticleInfo
                .Take(chunksize * 2)
                .Distinct()
                .OrderByDescending(d => d.ArticleDateWrit)
                .ToListAsync();

            
            foreach(ArticleInfo ai in dbEntity)
            {
                //ArticleCaption not in meta table
                ai.ArticleCaption = await this.GetArticleCaption(ai.ArticleIdFK);
            }

            return assignChunks(dbEntity, chunksize);
        }

        public async Task <IList<IList<ArticleInfo>>> GetArticleInfoByTag(string tag, int chunksize)
        {
            var dbEntity = await _context.ArticleInfo
                .Where(c => c.ArticleTags == tag)
				.Take(chunksize * 2)
                .Distinct()
                .OrderByDescending(d => d.ArticleDateWrit)
                .ToListAsync();

            //ArticleCaption not in meta table
            foreach(ArticleInfo ai in dbEntity)
            {
                ai.ArticleCaption = await this.GetArticleCaption(ai.ArticleIdFK);
            }

            return assignChunks(dbEntity, chunksize);
        }

        public async Task <Article> GetArticleTextById(int id)
        {
            var dbEntity = await _context.Article
            .SingleOrDefaultAsync(m => m.ArticleId == id);

            var entry = await _context.ArticleInfo
            .SingleOrDefaultAsync(m => m.ArticleIdFK == id);

            entry.ArticleViews++;
            var new_entry = _context.Entry(entry);
            new_entry.Property(e => e.ArticleViews).IsModified = true;

            await _context.SaveChangesAsync();


            return dbEntity;
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

            //var entry = await _context.ArticleInfo
                //.SingleOrDefaultAsync(m => m.ArticleIdFK == id);
            //entry.ArticleViews++;
            //var new_entry = _context.Entry(entry);
            //new_entry.Property(e => e.ArticleViews).IsModified = true;            
            //await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(ArticleInfo c)
        {
            //_context.SiteContent.Remove(c);
            //await _context.SaveChangesAsync();
        }

        private async Task <string> GetArticleCaption(int id)
        {
            var dbEntity = await _context.Article
            .SingleOrDefaultAsync(m => m.ArticleId == id);

            return dbEntity.ArticleCaption;
        }

        private IList<IList<ArticleInfo>> assignChunks(IList<ArticleInfo> articleInfoList, int chunksize)
        {
            //https://codereview.stackexchange.com/questions/90195/generic-method-to-split-provided-collection-into-smaller-collections

            var chunks = new List<IList<ArticleInfo>>();

            //chunkCount should always be 2 unless DB runs out of articles
            var chunkCount = articleInfoList.Count() / chunksize;

            //If remainder is greater than 0, then add to chunkCount
            if (articleInfoList.Count % chunksize > 0)
                chunkCount++;
            
            for (var i = 0; i < chunkCount; i++)
            {
                chunks.Add(articleInfoList.Skip(i * chunksize).Take(chunksize).ToList());
            }
            
            return chunks;
        }
    }
}