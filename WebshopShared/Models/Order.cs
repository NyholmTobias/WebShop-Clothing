using System;
using System.Collections.Generic;
using WebshopShared.Interfaces;

namespace WebshopShared.Models
{
    public enum Statuses
    {
        Accepted = 1,
        Payed = 2,
        Shipped = 3,
        Delivered = 4,
        Canceled = 5
    }
    public class Order : ITracking
    {
        public Guid OrderId { get; set; }

        public ICollection<LineItem> LineItems { get; set; }

        public Guid UserId { get; set; }

        public string Username { get; set; }

        public Statuses Status { get; set; }
        public double TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
