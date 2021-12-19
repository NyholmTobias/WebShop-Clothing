using System;
using System.Collections.Generic;

namespace WebshopShared.Models
{
    public class Item 
    {
        public Guid ItemId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }
        public string Description { get; set; }
        public bool Published { get; set; }
        public string PictureSourcePath { get; set; }
        public ICollection<LineItem> LineItems { get; set; }
        public ICollection<Category> Categories { get; set; }

    }
}
