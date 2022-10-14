using DataApi.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace WebAssemblyApp.Components
{
    public partial class LoginForm : ComponentBase
    {
        [Inject]
        public HttpClient HttpClient { get; set; }

        private User _model = new User();

        private async Task LoginUserAsync()
        {
            throw new NotImplementedException();
        }
    }
}
