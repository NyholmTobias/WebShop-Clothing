using System;
using System.Collections.Generic;

namespace WebshopShared.ResponseModels
{
    public class CategoryResponse : BaseResponse
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }

        public List<ItemResponse> Items { get; set; }
    }
}
