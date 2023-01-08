using Client.Services.Exceptions;
using Client.Services.Interfaces;
using DataApi.Shared.Models;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace Client.Services
{
    public class HttpPlanService : IPlanService
    {
        private readonly HttpClient _client;

        public HttpPlanService(HttpClient client)
        {
            _client = client;
        }

        public async Task<PagedList<Plan>> GetPlanAsync(string query, int pageNumber = 1, int pageSize = 10)
        {
            var @params = new Dictionary<string, string>
            {
                { "query", query},
                { "page", pageNumber.ToString()},
                { "pageSize", pageSize.ToString()},
            };

            var response = await _client.GetAsync(QueryHelpers.AddQueryString("/api/Plan", @params));
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<PagedList<Plan>>();
            }
            else
            {
                throw new ApiExeption(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<Plan> EditAsync(Plan plan)
        {
            var myContent = JsonConvert.SerializeObject(plan);
            var response = await _client.PutAsync($"/api/plan/{plan.Id}", new StringContent(myContent, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Plan>();
            }
            else
            {
                throw new ApiExeption(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<Plan> CreateAsync(Plan plan)
        {
            var myContent = JsonConvert.SerializeObject(plan);
            var response = await _client.PostAsync("/api/plan", new StringContent(myContent, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Plan>();
            }
            else
            {
                throw new ApiExeption(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<Plan> GetByIdAsync(string id)
        {
            var response = await _client.GetAsync($"/api/plan/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Plan>();
            }
            else
            {
                throw new ApiExeption(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task DeleteAsync(string id)
        {
            var response = await _client.DeleteAsync($"/api/plan/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiExeption(await response.Content.ReadAsStringAsync());
            }
        }
    }
}
