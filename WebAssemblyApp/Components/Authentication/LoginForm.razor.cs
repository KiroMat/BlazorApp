using Blazored.LocalStorage;
using DataApi.Shared.Models;
using DataApi.Shared.Responses;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using WebAssemblyApp.Infrastructure;

namespace WebAssemblyApp.Components
{
    public partial class LoginForm : ComponentBase
    {
        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public ILocalStorageService Storage { get; set; }


        private User _model = new User();
        private bool _isBusy = false;
        private string _errorMessage = string.Empty;

        private async Task LoginUserAsync()
        {
            _isBusy = true;
            _errorMessage = string.Empty;

            var response = await HttpClient.PostAsJsonAsync("/api/user/login", _model);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
                // Store it in local storage 
                await Storage.SetItemAsStringAsync(LocalStorageKeys.AccesToken, result?.Token);

                await AuthenticationStateProvider.GetAuthenticationStateAsync();

                Navigation.NavigateTo("/");
            }
            else
            {
                _errorMessage = "Wrong!";
            }
            _isBusy = false;
        }

        private void RedirectToRegister()
        {
            Navigation.NavigateTo("/authentication/register");
        }
    }
}
