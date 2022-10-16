using DataApi.Shared.Models;

namespace Client.Services.Interfaces
{
    public interface IPlanService
    {
        Task<PagedList<Plan>> GetPlanAsync(string query, int pageNumber = 1, int pageSize = 10);
    }
}
