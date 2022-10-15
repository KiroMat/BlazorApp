using DataApi.Shared.Models;

namespace Client.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task RegisterUserAsync(User user, CancellationToken cancellationToken);
    }
}
