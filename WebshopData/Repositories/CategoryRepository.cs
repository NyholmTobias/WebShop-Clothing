using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebshopShared.IRepository;
using WebshopShared.Models;

namespace WebshopData.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        protected new readonly WebshopDbContext _WebshopDbContext;
        public CategoryRepository(WebshopDbContext webshopDbContext) : base(webshopDbContext)
        {
            _WebshopDbContext = webshopDbContext;
        }

        public async Task<Category> GetCategoryByName(string categoryName)
        {
            return await _WebshopDbContext.Categories
                .Include(category => category.Items)
                .FirstOrDefaultAsync(category => category.Name == categoryName);
        }

    }
}
