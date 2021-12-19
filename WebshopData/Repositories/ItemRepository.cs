using WebshopShared.IRepository;
using WebshopShared.Models;

namespace WebshopData.Repositories
{
    public class ItemRepository : BaseRepository<Item>, IItemRepository
    {
        protected new readonly WebshopDbContext _WebshopDbContext;
        public ItemRepository(WebshopDbContext webshopDbContext) : base(webshopDbContext)
        {
            _WebshopDbContext = webshopDbContext;
        }
    }
}
