using System.Collections.Generic;
using System.Threading.Tasks;
using WebshopShared.ResponseModels;

namespace WebshopUI.Services.Categories
{
    public interface ICategoriesDataService
    {
        Task<List<CategoryResponse>> GetAllCategories();

        Task<CategoryResponse> GetCategoryByName(string categoryName);
    }
}
