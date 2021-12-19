using System;
using System.ComponentModel.DataAnnotations;
using WebshopShared.Interfaces;

namespace WebshopShared.RequestModels
{
    public class ItemRequest : IValidatable
    {
        public Guid ItemId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        public int StockQuantity { get; set; }
        [Required]
        public string Description { get; set; }
        public bool Published { get; set; }
        [Required]
        public string PictureSourcePath { get; set; }
    }
}
