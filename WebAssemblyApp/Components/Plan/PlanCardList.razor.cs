using AKSoftware.Blazor.Utilities;
using DataApi.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace WebAssemblyApp.Components
{
    public partial class PlanCardList
    {
        [Parameter]
        public Func<string, int, int, Task<PagedList<Plan>>> FetchPlans { get; set; }

        [Parameter]
        public EventCallback<Plan> OnViewClicked { get; set; }

        [Parameter]
        public EventCallback<Plan> OnEditClicked { get; set; }

        [Parameter]
        public EventCallback<Plan> OnDeleteClicked { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        private bool _isBusy { get; set; }
        private string _query = string.Empty;
        private int _pageNumber = 1;
        private int _pageSize = 2;
        private PagedList<Plan> _result = new();

        protected override void OnInitialized()
        {
            MessagingCenter.Subscribe<PlansList, Plan>(this, "plan_deleted", async (sender, args) =>
            {
                await GetPlansAsync(_pageNumber);
                StateHasChanged();
            });
        }

        protected async override Task OnInitializedAsync()
        {
            await GetPlansAsync();
        }

        async Task GetPlansAsync(int pageNumber = 1)
        {
            _pageNumber = pageNumber;
            _isBusy = true;
            _result = await FetchPlans?.Invoke(_query, _pageNumber, _pageSize);
            _isBusy = false;
        }

    }
}
