using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Interfaces;
using Shop.Application.Services;
using Shop.Data.Repository;
using Shop.Domain.Interfaces;

namespace Shop.Ioc
{
    public static class DependencyContainer
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // repository
            services.AddScoped<IAccountRepository, AccountRepository>();

            // service
            services.AddScoped<IAccountService, AccountService>();

        }
    }
}
