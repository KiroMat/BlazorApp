using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebAssemblyApp.Infrastructure
{
    public class JwtAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _storage;

        public JwtAuthenticationStateProvider(ILocalStorageService localStorageService)
        {
            _storage = localStorageService;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if(await _storage.ContainKeyAsync(LocalStorageKeys.AccesToken))
            {
                var tokenAsString = await _storage.GetItemAsStringAsync(LocalStorageKeys.AccesToken);
                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.ReadJwtToken(tokenAsString);
                var identity = new ClaimsIdentity(token.Claims, "Bearer");
                var user = new ClaimsPrincipal(identity);
                var authState = new AuthenticationState(user);

                NotifyAuthenticationStateChanged(Task.FromResult(authState));

                return authState;
            }

            return new AuthenticationState(new ClaimsPrincipal());      // Emty claims 
            //return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim("Id", "123") }, "Bearer")));
        }
    }
}
