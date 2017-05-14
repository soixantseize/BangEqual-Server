using System.Collections.Generic;
using System.Threading.Tasks;
using BareMetalApi.Models;

namespace BareMetalApi.Repositories.Interfaces
{
    public interface IShopDesignRepository
    {
        //Task<IEnumerable<BlogArticle>> TestGetAll();
        Task<bool> DoesItemExist(int id);

        Task<IList<ShopDesign>> GetAll();

        Task<ShopDesign> GetById(int id);

        Task<int> AddAsync(ShopDesign shopdesign);

        Task UpdateAsync(ShopDesign shopdesign);

        Task DeleteAsync(ShopDesign shopdesign);
    }
}