using System.Collections.Generic;
using System.Threading.Tasks;
using BangEqualServer.Models;

namespace BangEqualServer.Repositories.Interfaces
{
    public interface IArticleInfoRepository
    {
        //Task<IEnumerable<BlogArticle>> TestGetAll();
        Task<bool> DoesItemExist(int id);

        Task<IList<IList<ArticleInfo>>> GetArticle(string type, int chunksize);

        Task<IList<IList<ArticleInfo>>> GetByTopicAndType(string topic, int chunksize, string type);

        Task<IList<string>> GetTopic(string type);

        Task<ArticleInfo> GetById(int id);

        Task<int> AddAsync(ArticleInfo sitecontent);

        Task UpdateAsync(ArticleInfo sitecontent);

        Task DeleteAsync(ArticleInfo sitecontent);
    }
}