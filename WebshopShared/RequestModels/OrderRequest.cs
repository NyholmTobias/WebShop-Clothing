using System;
using System.Collections.Generic;
using WebshopShared.Interfaces;
using WebshopShared.Models;

namespace WebshopShared.RequestModels
{
    public class OrderRequest : IValidatable
    {
        public OrderRequest()
        {
            LineItems = new List<LineItemRequest>();
        }
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
        public string Username { get; set; }

        public ICollection<LineItemRequest> LineItems { get; set; }

        public Statuses Status { get; set; }
    }
}
