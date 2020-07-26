using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using VehicleRecords.Models;

namespace VehicleRecords
{
   public class Startup
   {
      //   F i e l d s   &   P r o p e r t i e s

      private readonly IConfiguration _configuration;

      //   C o n s t r u c t o r s

      public Startup(IConfiguration configuration)
      {
         _configuration = configuration;
      }

      //   M e t h o d s

      public void ConfigureServices(IServiceCollection services)
      {
         services.AddDbContext<AppDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
         // services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("VehicleRecordsDbConnectionString")));

         services.AddScoped<IFillupRepository, EfFillupRepository>();
         services.AddScoped<IMaintenanceRepairRepository, EfMaintenanceRepairRepository>(); 
         services.AddScoped<IUserRepository, EfUserRepository>();
         services.AddScoped<IVehicleRepository, EfVehicleRepository>();

         services.AddControllersWithViews();

         services.AddHttpContextAccessor();

         services.AddMemoryCache();
         services.AddSession();
         // services.AddSession(options =>
         // {
         //    options.Cookie.HttpOnly = true;
         //    options.Cookie.IsEssential = true;
         //    options.IdleTimeout = TimeSpan.FromSeconds(10);
         // });
      } // end ConfigureServices( )

      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }
         else
         {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
         }

         app.UseHttpsRedirection();
         app.UseStaticFiles();

         app.UseRouting();

         app.UseSession();

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{param?}");
         });
      }
   }
}
