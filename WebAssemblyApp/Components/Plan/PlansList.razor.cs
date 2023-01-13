using Client.Services.Exceptions;
using Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using DataApi.Shared.Models;
using MudBlazor;
using Microsoft.AspNetCore.Hosting.Server;
using AKSoftware.Blazor.Utilities;

namespace WebAssemblyApp.Components
{
    public partial class PlansList
    {
        [Inject]
        public IPlanService PlanService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        private bool _isBusy = false;
        private string _errorMessage = string.Empty;
        private string _query = string.Empty;
        private int _pageNumber = 1;
        private int _pageSize = 10;
        private int _totalPages = 10;

        private List<Plan> _plans = new();


        private async Task<PagedList<Plan>> GetPlansAsync(string query, int pageNumber = 1, int pageSize = 10)
        {
            _isBusy = true;

            try
            {
                var result = await PlanService.GetPlanAsync(query, pageNumber, pageSize);
                _plans = result.Records;
                _pageNumber = result.Page;
                _pageSize = result.PageSize;
                _totalPages = result.TotalPages;
                return result;
            }
            catch (ApiExeption ex)
            {
                _errorMessage = ex.Message;
            }


            _isBusy = false;
            return new();
        }

        private void EditPlan(Plan plan)
        {
            Navigation.NavigateTo($"/plans/form/{plan.Id}");
        }

        private async Task DeletePlan(Plan plan)
        {
            var parameters = new DialogParameters();
            parameters.Add("ContentText", $"Do you really want to delete the plan '{plan.Title}'?");
            parameters.Add("ButtonText", "Delete");
            parameters.Add("Color", Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = DialogService.Show<ConfirmationDialog>("Delete", parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                try
                {
                    await PlanService.DeleteAsync(plan.Id);
                    MessagingCenter.Send(this, "plan_deleted", plan);
                }
                catch (Exception ex)
                {

                    _errorMessage = ex.Message;
                }
                
            }
        }

        private void ViewPlan(Plan plan)
        {
            var parameters = new DialogParameters();
            parameters.Add("PlanId", plan.Id);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Medium };

            var dialog = DialogService.Show<PlanDetailsDialog>("Details", parameters, options);
        }
    }
}
