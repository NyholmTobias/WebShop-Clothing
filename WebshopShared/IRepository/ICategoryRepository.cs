using System.Threading.Tasks;
using WebshopShared.Models;

namespace WebshopShared.IRepository
{
    public interface ICategoryRepository : IAsyncRepository<Category>
    {
        Task<Category> GetCategoryByName(string categoryName);
    }
}
