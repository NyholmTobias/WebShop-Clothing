using AutoMapper;
using WebshopShared.DTOModels;
using WebshopShared.Models;
using WebshopShared.RequestModels;
using WebshopShared.ResponseModels;

namespace WebshopServices
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Order Maps
            CreateMap<OrderResponse, OrderRequest>();
            CreateMap<OrderResponse, Order>();
            CreateMap<Order, OrderRequest>();
            CreateMap<Order, OrderResponse>();
            CreateMap<OrderRequest, Order>();
            CreateMap<Order, DTOOrder>();

            //Item Maps
            CreateMap<ItemResponse, ItemRequest>();
            CreateMap<Item, ItemRequest>();
            CreateMap<Item, ItemResponse>();
            CreateMap<ItemRequest, Item>();
            CreateMap<Item, DTOItem>();
            CreateMap<ItemRequest, ItemResponse>();


            //Category Maps
            CreateMap<CategoryResponse, CategoryRequest>();
            CreateMap<Category, CategoryRequest>();
            CreateMap<Category, CategoryResponse>();
            CreateMap<CategoryRequest, Category>();
            CreateMap<Category, DTOCategory>();
            CreateMap<CategoryRequest, CategoryResponse>();

            //LineItem Maps
            CreateMap<LineItem, LineItemRequest>();
            CreateMap<LineItem, LineItemResponse>();
            CreateMap<DTOLineItem, LineItem>();
            CreateMap<LineItem, DTOLineItem>();
            CreateMap<LineItemRequest, LineItem>();
        }
    }
}
