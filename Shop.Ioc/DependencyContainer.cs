using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Convertors;
using Shop.Application.Interfaces;
using Shop.Application.Senders;
using Shop.Application.Services;
using Shop.Data.Repository;
using Shop.Domain.Interfaces;

namespace Shop.Ioc
{
    public static class DependencyContainer
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            #region repository

            services.AddScoped<IAccountRepository, AccountRepository>();

            #endregion

            #region service

            services.AddScoped<IAccountService, AccountService>();

            #endregion

            #region email

            services.AddTransient<IViewRenderService, RenderViewToString>();
            services.AddTransient<ISendEmailSerivce, SendEmailSerivce>();

            #endregion

        }
    }
}
