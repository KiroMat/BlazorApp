using Client.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Client.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterHttpServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, HttpAuthenticationService>()
                .AddScoped<IPlanService, HttpPlanService>()
                .AddScoped<IToDoItemService, HttpToDoItemService>()
                .AddScoped<IFileOperationService, HttpFileOperationService>();

            return services;
        }
    }
}
