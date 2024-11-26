using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Lamazon.DataAccess.Context;
using Lamazon.DataAccess.Implementations;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Services.Implementations;
using Lamazon.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Lamazon.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add serilog
            builder.Services.AddSerilog(options => 
            {
                options.MinimumLevel
                    .Error()
                    .WriteTo.File("logs.txt");
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add DB Context
            builder.Services.AddDbContext<LamazonDBContext>(options => 
            {
                // Add connection string here...
            });

            builder.Services.AddNotyf(config => 
            { 
                config.DurationInSeconds = 10; 
                config.IsDismissable = true; 
                config.Position = NotyfPosition.BottomRight; 
            });

            // Add Repositories
            builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();

            // Add Services
            builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IOrderItemService, OrderItemService>();

            builder.Services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opt => 
                { 
                    opt.ExpireTimeSpan = TimeSpan.FromDays(1);
                    opt.LoginPath = "/User/Login";
                });

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

            Stripe.StripeConfiguration.ApiKey = builder
                .Configuration
                .GetSection("Stripe:SecretKey")
                .Get<string>();

            app.UseRouting();

            app.UseAuthentication();  
            app.UseAuthorization();

            app.UseNotyf();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
