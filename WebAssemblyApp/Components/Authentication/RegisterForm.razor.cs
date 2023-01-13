using Client.Services.Exceptions;
using Client.Services.Interfaces;
using DataApi.Shared.Models;
using Microsoft.AspNetCore.Components;
using WebAssemblyApp.Shared;

namespace WebAssemblyApp.Components
{
    public partial class RegisterForm : ComponentBase
    {
        [Inject]
        public IAuthenticationService Service { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }

        private User _model = new User();
        private bool _isBusy = false;
        private string _errorMessage = string.Empty;

        private async Task RegisterUserAsync()
        {
            _isBusy = true;
            _errorMessage = string.Empty;

            try
            {
                await Service.RegisterUserAsync(_model, CancellationToken.None);
                Navigation.NavigateTo("/authentication/login");
            }
            catch (ApiExeption ex)
            {
                Error.HandlerError(ex);
                _errorMessage = ex.Message;
            }
            
            
            _isBusy = false;
        }

        private void RedirectToLogin()
        {
            Navigation.NavigateTo("/authentication/login");
        }
    }
}
