using System;
using System.Collections.Generic;
using WebshopShared.Models;

namespace WebshopShared.ResponseModels
{
    public class OrderResponse : BaseResponse
    {
        public OrderResponse()
        {
            LineItems = new List<LineItemResponse>();
        }
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public double TotalPrice { get; set; }

        public ICollection<LineItemResponse> LineItems { get; set; }

        public Statuses Status { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
