using Client.Services.Exceptions;
using Client.Services.Interfaces;
using DataApi.Shared.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace Client.Services
{
    public class HttpToDoItemService : IToDoItemService
    {
        private readonly HttpClient _client;

        public HttpToDoItemService(HttpClient client)
        {
            _client = client;
        }

        public async Task<ToDoItem> CreateAsync(ToDoItem item)
        {
            var myContent = JsonConvert.SerializeObject(item);
            var response = await _client.PostAsync("/api/ToDoItem", new StringContent(myContent, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ToDoItem>();
            }
            else
            {
                throw new ApiExeption(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task DeleteAsync(string id)
        {
            var response = await _client.DeleteAsync($"/api/ToDoItem/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiExeption(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<ToDoItem> EditAsync(ToDoItem item)
        {
            var myContent = JsonConvert.SerializeObject(item);
            var response = await _client.PutAsync($"/api/ToDoItem/{item.Id}", new StringContent(myContent, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ToDoItem>();
            }
            else
            {
                throw new ApiExeption(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<ToDoItem> GetByIdAsync(string id)
        {
            var response = await _client.GetAsync($"/api/ToDoItem/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ToDoItem>();
            }
            else
            {
                throw new ApiExeption(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<List<ToDoItem>> GetItemsByPlanIdAsync(string planId)
        {
            var response = await _client.GetAsync($"/api/ToDoItem/plan/{planId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<ToDoItem>>();
            }
            else
            {
                throw new ApiExeption(await response.Content.ReadAsStringAsync());
            }
        }
    }
}
