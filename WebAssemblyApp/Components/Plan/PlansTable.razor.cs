using Microsoft.AspNetCore.Components;
using Client.Services.Interfaces;
using DataApi.Shared.Models;
using MudBlazor;
using AKSoftware.Blazor.Utilities;

namespace WebAssemblyApp.Components
{
    public partial class PlansTable
    {
        [Inject]
        public IPlanService PlanService { get; set; }

        [Parameter]
        public EventCallback<Plan> OnViewClicked { get; set; }

        [Parameter]
        public EventCallback<Plan> OnDeleteClicked { get; set; }

        [Parameter]
        public EventCallback<Plan> OnEditClicked { get; set; }

        private string _query = string.Empty;
        private MudTable<Plan> _table;

        protected override void OnInitialized()
        {
            MessagingCenter.Subscribe<PlansList, Plan>(this, "plan_deleted", async (sender, args) =>
            {
                await _table.ReloadServerData();
                StateHasChanged();
            });
        }

        private async Task<TableData<Plan>> ServerReloadAsync(TableState state)
        {
            var result = await PlanService.GetPlanAsync(_query, state.Page, state.PageSize);

            return new TableData<Plan>
            {
                Items = result.Records,
                TotalItems = result.ItemsCount
            };
        }

        private void OnSearch(string query)
        {
            _query = query;
            _table.ReloadServerData();   
        }
    }
}