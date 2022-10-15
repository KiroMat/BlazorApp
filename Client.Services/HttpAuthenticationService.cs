using Client.Services.Exceptions;
using Client.Services.Interfaces;
using DataApi.Shared.Models;
using DataApi.Shared.Responses;
using System.Net;
using System.Net.Http.Json;

namespace Client.Services
{
    public class HttpAuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _client;

        public HttpAuthenticationService(HttpClient httpClient)
        {
            _client = httpClient;
        }

        public async Task RegisterUserAsync(User user, CancellationToken cancellationToken)
        {
            var response = await _client.PostAsJsonAsync("/api/user", user, cancellationToken);
            if(!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ErrorApiResponse>();
                throw new ApiExeption(errorResponse, (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), errorResponse.Status.ToString()));
            }
        }
    }
}
