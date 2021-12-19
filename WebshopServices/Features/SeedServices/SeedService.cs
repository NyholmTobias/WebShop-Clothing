using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebshopData;
using WebshopServices.Features.Interfaces;
using WebshopShared.IRepository;
using WebshopShared.Models;

namespace WebshopServices.Features.SeedServices
{
    public class SeedService : ISeedService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IOrderRepository _orderRepository;
        public SeedService(ICategoryRepository categoryRepository, IItemRepository itemRepository, IOrderRepository orderRepository)
        {
            _categoryRepository=categoryRepository;
            _itemRepository=itemRepository;
            _orderRepository=orderRepository;
        }
       
        public async Task CreateSeed()
        {
            //Om det finns någon order I DB så seedas det inte på nytt
            var orders = await _orderRepository.ListAllAsync();
            if (orders.Count > 0)
            {
                return;
            }
            var aliceAccoutGuid = Guid.Parse("1b5e052e-d2ad-4d11-a9c8-9d4af9280ada");
            var bobAccoutGuid = Guid.Parse("67970821-2239-4732-93cf-8cc8b9d7f46f");
            var adminAccoutGuid = Guid.Parse("d2f46ad5-4f40-4aa4-8fa7-9dd75db8aeb1");
            var customerAccoutGuid = Guid.Parse("e67bed71-fc3d-4c77-8029-6989967e8997");

            var categoryRobin = new Category { Name = "Robin" };
            var categoryPontus = new Category { Name = "Pontus" };
            var categoryRebecca = new Category { Name = "Rebecca" };
            var categoryTobias = new Category { Name = "Tobias" };

            var robinToList = new List<Category> { categoryRobin };
            var PontusToList = new List<Category> { categoryPontus };
            var RebeccaToList = new List<Category> { categoryRebecca };
            var TobiasToList = new List<Category> { categoryTobias };

            var robinItem1 = new Item { Name = "Robins Face", Description = "Svart tröja med Robins face!", Price=100, StockQuantity= 25, ItemId = Guid.NewGuid(), Categories = robinToList, PictureSourcePath = "images/robin_individuell.jpg" };
            var robinItemGroup1 = new Item { Name = "Robins Gruppbild", Description = "Svart tröja med hela gruppen!", Price=500, StockQuantity= 10, Categories = robinToList, PictureSourcePath = "images/gruppbild.jpg" };


            var PontusItem1 = new Item { Name = "Pontus Face", Description = "Svart tröja med Pontus face!", Price=100, StockQuantity= 25, Categories = PontusToList, PictureSourcePath = "images/pontus_individuell.jpg" };
            var PontusItemGroup1 = new Item { Name = "Pontus Gruppbild", Description = "Svart tröja med hela gruppen!", Price=500, StockQuantity= 10, Categories = PontusToList, PictureSourcePath = "images/gruppbild.jpg" };

            var RebeccaItem1 = new Item { Name = "Rebeccas Face", Description = "Svart tröja med Rebecca face!", Price=100, StockQuantity= 25, Categories = RebeccaToList, PictureSourcePath = "images/rebecca_individuell.jpg" };
            var RebeccaItemGroup1 = new Item { Name = "Rebeccas Gruppbild", Description = "Svart tröja med hela gruppen!", Price=500, StockQuantity= 10, Categories = RebeccaToList, PictureSourcePath = "images/gruppbild.jpg" };

            var TobiasItem1 = new Item { Name = "Tobias Face", Description = "Svart tröja med Tobias face!", Price=100, StockQuantity= 25, Categories = TobiasToList, PictureSourcePath = "images/tobias_individuell.jpg" };
            var TobiasItemGroup1 = new Item { Name = "Tobias Gruppbild", Description = "Svart tröja med hela gruppen!", Price=500, StockQuantity= 10, Categories = TobiasToList, PictureSourcePath = "images/gruppbild.jpg" };

            await _categoryRepository.AddAsync(categoryRobin);
            await _categoryRepository.AddAsync(categoryPontus);
            await _categoryRepository.AddAsync(categoryRebecca);
            await _categoryRepository.AddAsync(categoryTobias);

            await _itemRepository.AddAsync(robinItem1);
            await _itemRepository.AddAsync(robinItemGroup1);

            await _itemRepository.AddAsync(PontusItem1);
            await _itemRepository.AddAsync(PontusItemGroup1);

            await _itemRepository.AddAsync(RebeccaItem1);
            await _itemRepository.AddAsync(RebeccaItemGroup1);

            await _itemRepository.AddAsync(TobiasItem1);
            await _itemRepository.AddAsync(TobiasItemGroup1);

            var robinsOrder = new Order { OrderId = Guid.NewGuid(), Status = Statuses.Shipped, UserId = aliceAccoutGuid, Username = "alice", TotalPrice = 1549 };
            var pontusOrder = new Order { OrderId = Guid.NewGuid(), Status = Statuses.Canceled, UserId = aliceAccoutGuid, Username = "alice", TotalPrice = 99 };
            var rebeccasOrder = new Order { OrderId = Guid.NewGuid(), Status = Statuses.Delivered, UserId = customerAccoutGuid, Username = "customer", TotalPrice = 249 };
            var tobiasOrder = new Order { OrderId = Guid.NewGuid(), Status = Statuses.Accepted, UserId = adminAccoutGuid, Username = "admin", TotalPrice = 3789 };

            var robinsLineItems = new List<LineItem>
            {
                new LineItem { Quantity = 2, ItemId = robinItem1.ItemId, OrderId = robinsOrder.OrderId },
                new LineItem { Quantity = 3, ItemId = robinItemGroup1.ItemId, OrderId = robinsOrder.OrderId }
            };

            var pontusLineItems = new List<LineItem>
            {
                new LineItem { Quantity = 5, ItemId = PontusItem1.ItemId, OrderId = pontusOrder.OrderId },
                new LineItem { Quantity = 1, ItemId = PontusItemGroup1.ItemId, OrderId = pontusOrder.OrderId }
            };

            var rebeccasLineItems = new List<LineItem>
            {
                new LineItem { Quantity = 7, ItemId = RebeccaItem1.ItemId, OrderId = rebeccasOrder.OrderId },
                new LineItem { Quantity = 3, ItemId = RebeccaItemGroup1.ItemId, OrderId = rebeccasOrder.OrderId }
            };

            var tobiasLineItems = new List<LineItem>
            {
                new LineItem { Quantity = 100, ItemId = TobiasItem1.ItemId, OrderId = tobiasOrder.OrderId },
                new LineItem { Quantity = 5, ItemId = TobiasItemGroup1.ItemId, OrderId = tobiasOrder.OrderId }
            };

            robinsOrder.LineItems = robinsLineItems;
            pontusOrder.LineItems = pontusLineItems;
            rebeccasOrder.LineItems = rebeccasLineItems;
            tobiasOrder.LineItems = tobiasLineItems;

            await _orderRepository.AddAsync(robinsOrder);
            await _orderRepository.AddAsync(pontusOrder);
            await _orderRepository.AddAsync(rebeccasOrder);
            await _orderRepository.AddAsync(tobiasOrder);
        }
    }
}
