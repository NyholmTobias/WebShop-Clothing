using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using WebshopShared.ResponseModels;
using WebshopUI.Components;
using WebshopUI.Services.Categories;

namespace WebshopUI.Pages
{
    public partial class CategoryPage : ComponentBase
    {
        [Inject]
        public ICategoriesDataService CategoryDataService { get; set; }

        [Parameter]
        public string CategoryName { get; set; }

        public CategoryResponse CategoryResponse { get; set; }
        
        private ItemCard ItemCard { get; set; }

        private SomethingWentWrong SomethingWentWrong { get; set; }
        protected override async Task OnInitializedAsync()
        {
            CategoryResponse = await CategoryDataService.GetCategoryByName(CategoryName);
        }
    }
}
