using ApiRefrigerator.Repository;
using Application.Services;
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IoC
{
    public static class DependencyInjection
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IRefrigeratorService, RefrigeratorService>();
            services.AddScoped<IRefrigeratorRepository, RefrigeratorRepository>();
        }
    }
}
