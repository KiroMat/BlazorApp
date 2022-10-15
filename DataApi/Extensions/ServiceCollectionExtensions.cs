using DataApi.Shared.Models;
using DataApi.Validators;
using FluentValidation;

namespace DataApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddValitators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<User>, UserValidator>();
            return services;
        }
    }
}
