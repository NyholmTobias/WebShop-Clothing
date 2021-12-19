using System;
using System.Collections.Generic;
using WebshopShared.Interfaces;
using WebshopShared.Models;

namespace WebshopShared.RequestModels
{
    public class CategoryRequest : IValidatable
    {   
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public List<ItemRequest> Items { get; set; }
    }
}
