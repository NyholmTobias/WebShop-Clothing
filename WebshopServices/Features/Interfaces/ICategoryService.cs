using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebshopShared.RequestModels;
using WebshopShared.ResponseModels;

namespace WebshopServices.Features.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryResponse> CreateCategory(CategoryRequest categoryRequest);
        Task<Guid> DeleteCategory(Guid categoryId);
        Task<CategoryResponse> UpdateCategory(CategoryRequest categoryRequest);
        Task<List<CategoryResponse>> GetAllCategories();
        Task<CategoryResponse> GetCategoryById(Guid categoryId);

        Task<CategoryResponse> GetCategoryByName(string cateGoryName);
    }
}
