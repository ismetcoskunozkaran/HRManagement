using HRProject_NTier.DATAACCESS.Context;
using HRProject_NTier.DATAACCESS.Repositories.Abstract;
using HRProject_NTier.DATAACCESS.Repositories.Concrete;
using HRProject_NTier.WEBUI.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRProject_NTier.WEBUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<ProjectContext>(options => options.UseSqlServer("Server=.; database=HRMDBFin; uid=sa; pwd=123"));



            services.AddTransient<IManagerRepository, ManagerRepository>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<IPersonnelRepository, PersonnelRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddTransient<IPackageRepository, PackageRepository>();
            services.AddTransient<IManagerCommentRepository, ManagerCommentRepository>();
          
            //ekle
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddTransient<ISpendingRepository, SpendingRepository>();
            services.AddSession();
           
            
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie
            //    (x =>
            //    {
            //        x.LoginPath = "/ManagerArea/Login/LoginForm";
            //    })
            //    ;//


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();
            //app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                   name: "AdminArea",
                   areaName: "AdminArea",
                   pattern: "AdminArea/{controller=Login}/{action=LoginForm}/{id?}"
                   );

                endpoints.MapAreaControllerRoute(
                    name: "PersonnelArea",
                    areaName: "PersonnelArea",
                    pattern: "PersonnelArea/{controller=Login}/{action=LoginForm}/{id?}"
                    );

                endpoints.MapAreaControllerRoute(
                    name: "ManagerArea",
                    areaName: "ManagerArea",
                    pattern: "ManagerArea/{controller=Login}/{action=LoginForm}/{id?}"
                    );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
