using Exchange_Rates.Controllers;
using Exchange_Rates.Service;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Exchange_Rates
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpClient("ExchangeRate", x =>
            {
                x.BaseAddress =new Uri("http://v6.exchangerate-api.com/v6/aae5451edb446b16928b6338/latest/USD");
            });
            builder.Services.AddScoped<IConsuming,Consuming>();
            builder.Services.AddScoped<ExceptionFilter>();
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
