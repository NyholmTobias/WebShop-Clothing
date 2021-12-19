using System;
using WebshopShared.Interfaces;

namespace WebshopShared.RequestModels
{
    public class LineItemRequest : IValidatable
    {
        public Guid ItemId { get; set; }

        public Guid OrderId { get; set; }

        public int Quantity { get; set; }

        public ItemRequest Item { get; set; }
    }
}
