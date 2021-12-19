using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace WebshopUI.Components
{
    public partial class LoadingComponent : ComponentBase
    {
        public bool YouAreLucky { get; set; }

        protected override async Task OnInitializedAsync()
        {
            YouAreLucky = true;
        }
    }
}
