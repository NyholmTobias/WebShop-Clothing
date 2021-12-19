using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebshopShared.Models;
using WebshopShared.ResponseModels;
using WebshopUI.Components;
using WebshopUI.Services.Categories;

namespace WebshopUI.Shared
{
    public partial class NavMenu : ComponentBase
    {
        [Inject]
        public NavigationManager NavManager { get; set; }


        [Parameter]
        public List<CategoryResponse> CategoryResponses { get; set; }

        [Inject]
        public ICategoriesDataService CategoriesDataService { get; set; }

        private LoginLogoutButton LoginLogoutButton { get; set; }

        protected async override Task OnInitializedAsync()
        {
            CategoryResponses = await CategoriesDataService.GetAllCategories();
        }


        public void NavigateToCategory(string categoryname)
        {
            NavManager.NavigateTo($"/category/{categoryname}", true);
            StateHasChanged();
        }
    }
}



