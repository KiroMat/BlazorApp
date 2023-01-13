using DataApi.Shared.Models;

namespace Client.Services.Interfaces
{
    public interface IToDoItemService
    {
        Task<List<ToDoItem>> GetItemsByPlanIdAsync(string planId);
        Task<ToDoItem> GetByIdAsync(string id);
        Task<ToDoItem> CreateAsync(ToDoItem item);
        Task<ToDoItem> EditAsync(ToDoItem item);
        Task DeleteAsync(string id);
    }
}
