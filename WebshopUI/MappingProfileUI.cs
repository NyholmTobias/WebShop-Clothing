using AutoMapper;
using DreamTeam.IDP.Shared.RequestModels;
using DreamTeam.IDP.Shared.ResponseModels;
using WebshopShared.RequestModels;
using WebshopShared.ResponseModels;

namespace WebshopUI
{
    public class MappingProfileUI : Profile
    {
        
            public MappingProfileUI()
            {
                //User mapping
                CreateMap<UserResponse, UserRequest>();
                CreateMap<UserRequest, UserResponse>();

                
                //Order mapping
                CreateMap<OrderResponse, OrderRequest>();
                CreateMap<OrderRequest, OrderResponse>();

                //Item mapping
                CreateMap<ItemResponse, ItemRequest>();
                CreateMap<ItemRequest, ItemResponse>();

                //Lineitem mapping
                CreateMap<LineItemResponse, LineItemRequest>();
                CreateMap<LineItemRequest, LineItemResponse>();

                //Category mapping
                CreateMap<CategoryRequest, CategoryResponse>().ReverseMap();

            }
        
    }
}
