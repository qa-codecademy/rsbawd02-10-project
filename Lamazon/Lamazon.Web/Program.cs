using Lamazon.DataAccess.Context;
using Lamazon.DataAccess.Implementations;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Services.Implementations;
using Lamazon.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lamazon.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add DB Context
            builder.Services.AddDbContext<LamazonDBContext>(options => 
            {
            });

            // Add Repositories
            builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();

            // Add Services
            builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();

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
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
