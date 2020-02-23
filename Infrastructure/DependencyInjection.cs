
using Application.Common.Interfaces;
using Infrastructure.Services;
using Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<HasherSettings>(options => configuration.GetSection(nameof(HasherSettings)).Bind(options));
            services.AddScoped<IHasherService, HasherService>();

            return services;
        }
    }
}