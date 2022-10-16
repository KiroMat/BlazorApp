using Client.Services.Exceptions;
using Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using DataApi.Shared.Models;

namespace WebAssemblyApp.Components
{
    public partial class PlansList
    {
        [Inject]
        public IPlanService PlanService { get; set; }

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
    }
}
