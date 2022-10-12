using DataApi.Shared.Models;
using DataApi.Shared.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DataApi.Shared
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
