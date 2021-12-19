using System;
using WebshopShared.Models;

namespace WebshopShared.DTOModels
{
    public class DTOOrder
    {
        public Guid OrderId { get; set; }

        public Statuses status { get; set; }
        public double TotalPrice { get; set; }
    }
}
