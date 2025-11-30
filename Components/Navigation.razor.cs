using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BookMakerPredictionsFE.Components
{
    public partial class Navigation
    {
        [Parameter]
        public EventCallback OnItemClick { get; set; }

        private async Task OnItemClicked(MouseEventArgs _)
        {
            if (OnItemClick.HasDelegate)
                await OnItemClick.InvokeAsync();
        }
    }
}
