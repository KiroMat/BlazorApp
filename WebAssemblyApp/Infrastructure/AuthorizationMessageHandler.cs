using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace WebAssemblyApp.Infrastructure
{
    public class AuthorizationMessageHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorage;

        private const string AccessTokenKey = "acces_token";

        public AuthorizationMessageHandler(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if(await _localStorage.ContainKeyAsync(AccessTokenKey))
            {
                var token = await _localStorage.GetItemAsStringAsync(AccessTokenKey);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            Console.WriteLine("Test call to API");
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
