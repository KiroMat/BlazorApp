using DataApi.Shared.Models;

namespace Client.Services.Interfaces
{
    public interface IPlanService
    {
        Task<PagedList<Plan>> GetPlanAsync(string query, int pageNumber = 1, int pageSize = 10);
        Task<Plan> GetByIdAsync(string id);
        Task<Plan> CreateAsync(Plan plan);
        Task<Plan> EditAsync(Plan plan);
        Task DeleteAsync(string id);
    }
}
