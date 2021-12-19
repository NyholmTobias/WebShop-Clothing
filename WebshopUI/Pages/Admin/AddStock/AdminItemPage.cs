using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebshopShared.ResponseModels;
using WebshopUI.Components;
using WebshopUI.Services.Items;

namespace WebshopUI.Pages.Admin.AddStock
{
    public partial class AdminItemPage : ComponentBase
    {
        [Inject]
        public IItemDataService ItemDataService { get; set; }

        public List<ItemResponse> Items { get; set; }

        private ItemCard ItemCard { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Items = await ItemDataService.GetAllItems();
        }



    }
}
