using System.Collections.Generic;
using System.Threading.Tasks;
using BangEqualServer.Models;

namespace BangEqualServer.Repositories.Interfaces
{
    public interface IArticleInfoRepository
    {
        Task<bool> DoesItemExist(int id);

        Task<IList<IList<ArticleInfo>>> GetArticleInfo(int chunksize);

        Task<IList<IList<ArticleInfo>>> GetArticleInfoByTag(string tag, int chunksize);
		
		Task<IList<string>> GetArticleInfoTags();

        Task<int> AddAsync(ArticleInfo sitecontent);

        Task UpdateAsync(ArticleInfo sitecontent);

        Task DeleteAsync(ArticleInfo sitecontent);
    }
}