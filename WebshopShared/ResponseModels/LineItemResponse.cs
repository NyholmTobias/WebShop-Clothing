using System;
using WebshopShared.DTOModels;

namespace WebshopShared.ResponseModels
{
    public class LineItemResponse : BaseResponse
    {
        public Guid ItemId { get; set; }

        public Guid OrderId { get; set; }

        public int Quantity { get; set; }

        public OrderResponse Order { get; set; }
        public ItemResponse Item { get; set; }
    }
}
