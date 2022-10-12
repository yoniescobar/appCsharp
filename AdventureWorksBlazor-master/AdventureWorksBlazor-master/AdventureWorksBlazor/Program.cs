using AdventureWorksBlazor.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using AdventureWorksNS.Data;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksBlazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();
            
            //Agregar el servicio de AdventureWorks
            builder.Services.AddDbContext<AdventureWorksDB>(options =>
                options.UseSqlServer("Server=localhost;Initial Catalog=AdventureWorksLT2019;Integrated Security=false;User=soporte;Password=12003906;"),
                ServiceLifetime.Transient);
            //Reconozca la fuente de datos de Customer
            builder.Services.AddTransient<ICustomerService, CustomerService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}