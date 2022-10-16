using Client.Services.Exceptions;
using Client.Services.Interfaces;
using DataApi.Shared.Models;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;

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
            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<PagedList<Plan>>();
            }
            else
            {
                throw new ApiExeption(await response.Content.ReadAsStringAsync());
            }
        }
    }
}
