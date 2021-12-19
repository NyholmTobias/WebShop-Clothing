using System;
using System.Collections.Generic;
using WebshopShared.Models;
using WebshopShared.RequestModels;

namespace WebShopServiceTests
{
    public class TestHelper
    {
        public OrderRequest CreateOrderRequest() => new OrderRequest
        {
            OrderId = Guid.NewGuid(),
            LineItems = new List<LineItemRequest>
                {
                    CreateLineItemRequest(),
                    CreateLineItemRequest()
                },
            Status = Statuses.Accepted
        };

        public LineItemRequest CreateLineItemRequest() => new LineItemRequest
        {
            Item = CreateItemRequest(),
            Quantity = 10,
        };

        public ItemRequest CreateItemRequest() => new ItemRequest
        {
            Name = "Name",
            Price = 10.2,
            StockQuantity = 6,
            Description = "Description",
            Published = true,
            PictureSourcePath = "~images/test.png"
        };

        public CategoryRequest CreateCategoryRequest()
        {
            var categoryRequest = new CategoryRequest
            {
                Name = "CategoryRequest",
                Items = new List<ItemRequest>
                {
                    new ItemRequest {},
                    new ItemRequest {}
                }

            };

            return categoryRequest;
        }

        public Category CreateCategory()
        {
            var category = new Category
            {
                Name = "Category",
                Items = new List<Item>
                {
                    new Item()
                }

            };

            return category;
        }

        public LineItem CreateLineItem()
        {
            var itemId = new Guid();

            var lineItem = new LineItem
            {
                Quantity = 10,
                ItemId = itemId,
                Item = new Item
                {
                    ItemId = itemId,
                    StockQuantity = 12,
                }
            };
            return lineItem;
        }
    }
}
