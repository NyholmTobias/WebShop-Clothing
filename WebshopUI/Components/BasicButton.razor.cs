using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace WebshopUI.Components
{
    public partial class BasicButton : ComponentBase
    {
        [Parameter]
        public string ButtonText { get; set; } = "ButtonText";
    }
}
