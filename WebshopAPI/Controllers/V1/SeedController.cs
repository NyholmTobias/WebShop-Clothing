using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebshopServices.Features.Interfaces;

namespace WebshopAPI.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SeedController : Controller
    {
        private readonly ISeedService _seedService;
        public SeedController(ISeedService seedService)
        {
            _seedService=seedService;
        }

        [HttpPost]
        public async Task SeedDatabase()
        {
            await _seedService.CreateSeed();
        }
    }
}
