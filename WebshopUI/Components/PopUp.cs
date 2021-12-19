using Microsoft.AspNetCore.Components;

namespace WebshopUI.Components
{
    public partial class PopUp : ComponentBase
    {
        [Parameter]
        public string Title { get; set; }
        [Parameter]
        public string Message { get; set; }
        public bool Show { get; set; }
        public string Class { get; set; } = "container";

        public void Open()
        {
            Show = true;
            Class = "container";
        }
        public void Close()
        {
            Show = false;
            Class = "hide";
        }
    }
}
