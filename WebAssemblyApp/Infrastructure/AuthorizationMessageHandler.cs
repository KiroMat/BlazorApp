using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace WebAssemblyApp.Infrastructure
{
    public class AuthorizationMessageHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorage;

        public AuthorizationMessageHandler(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if(await _localStorage.ContainKeyAsync(LocalStorageKeys.AccesToken))
            {
                var token = await _localStorage.GetItemAsStringAsync(LocalStorageKeys.AccesToken);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            Console.WriteLine("Test call to API");
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
