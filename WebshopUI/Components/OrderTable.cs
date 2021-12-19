using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopShared.ResponseModels;
using WebshopUI.Components.Modals;
using WebshopUI.Services.Orders;

namespace WebshopUI.Components
{
    public partial class OrderTable : ComponentBase
    {
        [Parameter]
        public List<OrderResponse> OrderResponses { get; set; }
        [Parameter]
        public bool Admin { get; set; }

        [Parameter]
        public string NavigateToOnClick { get; set; }

        [Inject]
        public IOrderDataService OrderDataService { get; set; }

        public Pager Pager { get; set; }

        public IOrderedEnumerable<OrderResponse> OrderedOrderResponses { get; set; }

        private DeleteOrder DeleteOrder { get; set; }

        private bool OrderedByOrderNo = false;
        private bool OrderedByBoughtBy = false;
        private bool OrderedByCreatedDate = false;
        private bool OrderedByModifiedDate = false;
        private bool OrderedByStatus = false;


        [Parameter]
        public string Title { get; set; }
        

        public string OrderByOrderNo = "OrderNo";
        public string OrderByBoughtBy = "CreatedBy";
        public string OrderByCreatedDate = "CreatedDate";
        public string OrderByModifiedDate = "ModifiedDate";
        public string OrderByStatus = "OrderByStatus";
        public string LastSortedBy = "";



        protected override async Task OnInitializedAsync()
        {
            SortOrdersBy("CreatedDate");
        }

        public void ShowDeleteModal()
        {
            DeleteOrder.Show();
        }

        public async void DeleteModal_OnDialogClose()
        {
            OrderResponses = await OrderDataService.GetAllOrders();
            SortOrdersBy(LastSortedBy);
            StateHasChanged();
        }


        public void SortOrdersBy(string SortBy)
        {
            LastSortedBy=SortBy;

            switch (SortBy)
            {

                case "OrderNo":
                    if (!OrderedByOrderNo)
                    {
                        OrderedOrderResponses = OrderResponses.OrderBy(x => x.OrderId);
                        OrderedByOrderNo = true;
                    }
                    else
                    {
                        OrderedOrderResponses = OrderResponses.OrderByDescending(x => x.OrderId);
                        OrderedByOrderNo = false;
                    }
                    StateHasChanged();
                    break;

                case "CreatedBy":
                    if (!OrderedByBoughtBy)
                    {
                        OrderedOrderResponses = OrderResponses.OrderBy(x => x.Username);
                        OrderedByBoughtBy = true;
                    }
                    else
                    {
                        OrderedOrderResponses = OrderResponses.OrderByDescending(x => x.Username);
                        OrderedByBoughtBy = false;
                    }
                    StateHasChanged();
                    break;

                case "CreatedDate":
                    if (!OrderedByCreatedDate)
                    {
                        OrderedOrderResponses = OrderResponses.OrderBy(x => x.CreatedDate);
                        OrderedByCreatedDate = true;
                    }
                    else
                    {
                        OrderedOrderResponses = OrderResponses.OrderByDescending(x => x.CreatedDate);
                        OrderedByCreatedDate = false;
                    }
                    StateHasChanged();
                    break;
                case "ModifiedDate":
                    if (!OrderedByModifiedDate)
                    {
                        OrderedOrderResponses = OrderResponses.OrderBy(x => x.LastModifiedDate);
                        OrderedByModifiedDate = true;
                    }
                    else
                    {
                        OrderedOrderResponses = OrderResponses.OrderByDescending(x => x.LastModifiedDate);
                        OrderedByModifiedDate = false;
                    }
                    StateHasChanged();
                    break;

                case "OrderByStatus":
                    if (!OrderedByStatus)
                    {
                        OrderedOrderResponses = OrderResponses.OrderBy(x => x.Status);
                        OrderedByStatus = true;
                    }
                    else
                    {
                        OrderedOrderResponses = OrderResponses.OrderByDescending(x => x.Status);    
                        OrderedByStatus = false;
                    }
                    StateHasChanged();
                    break;

                default:
                    break;

            }
        }
    }
}
