using System.Collections.Generic;
using System.Threading.Tasks;
using BareMetalApi.Models;

namespace BareMetalApi.Repositories.Interfaces
{
    public interface IContentRepository
    {
        //Task<IEnumerable<BlogArticle>> TestGetAll();
        Task<bool> DoesItemExist(int id);

        Task<IList<IList<Content>>> GetContent(string type, int chunksize);

        Task<IList<IList<Content>>> GetByTopicAndType(string topic, int chunksize, string type);

        Task<IList<string>> GetTopic(string type);

        Task<Content> GetById(int id);

        Task<int> AddAsync(Content sitecontent);

        Task UpdateAsync(Content sitecontent);

        Task DeleteAsync(Content sitecontent);
    }
}