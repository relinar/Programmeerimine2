using KooliProjekt.PublicAPI;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace KooliProjekt.BlazorApp.Pages
{
    public partial class Home
    {
        [Inject]
        protected IApiClient apiClient { get; set; }

        [Inject]
        protected IJSRuntime JsRuntime { get; set; }

        [Inject]
        protected NavigationManager NavManager { get; set; }

        private List<Amount> Amount;

        protected override async Task OnInitializedAsync()
        {
            var result = await apiClient.List();

            Amount = result.Value;
        }

        protected async Task Delete(int id)
        {
            bool confirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Are you sure?");
            if (!confirmed)
            {
                return;
            }

            await apiClient.Delete(id);

            NavManager.Refresh();
        }
    }
}