using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Shop.Data.Context;
using Shop.Ioc;

namespace Shop.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            #region config database

            builder.Services.AddDbContext<ShopContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("ShopContext"));
            });

            #endregion

            #region ioc

            builder.Services.RegisterServices();

            #endregion

            #region authentication

            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme= CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme= CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultSignInScheme= CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(option =>
            {
                option.LoginPath = "/login";
                option.LogoutPath = "/log-out";
                option.ExpireTimeSpan=TimeSpan.FromDays(30);
            });


            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}