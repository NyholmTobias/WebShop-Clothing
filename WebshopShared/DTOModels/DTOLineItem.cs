using System;

namespace WebshopShared.DTOModels
{
    public class DTOLineItem
    {
        public Guid ItemId { get; set; }

        public Guid OrderId { get; set; }

        public int Quantity { get; set; }
    }
}
