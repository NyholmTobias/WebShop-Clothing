using WebshopShared.IRepository;
using WebshopShared.Models;

namespace WebshopData.Repositories
{
    public class LineItemRepository : BaseRepository<LineItem>, ILineItemRepository
    {
        protected new readonly WebshopDbContext _WebshopDbContext;
        public LineItemRepository(WebshopDbContext webshopDbContext) : base(webshopDbContext)
        {
            _WebshopDbContext = webshopDbContext;
        }
    }
}
