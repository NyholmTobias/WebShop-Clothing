using System;

namespace WebshopShared.Models
{
    public class LineItem
    {
        public Guid ItemId { get; set; }

        public Guid OrderId { get; set; }

        public int Quantity { get; set; }

        public Order Order { get; set; }
        public Item Item { get; set; }
    }
}
