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
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<ITicketMessageRepository, TicketMessageRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddScoped<IProductDiscountRepository, ProductDiscountRepository>();
            services.AddScoped<IProductGalleryRepository, ProductGalleryRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            #endregion

            #region service

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IRolePermissionService, RolePermissionService>();

            #endregion

            #region email

            services.AddTransient<IViewRenderService, RenderViewToString>();
            services.AddTransient<ISendEmailSerivce, SendEmailSerivce>();

            #endregion

        }
    }
}
