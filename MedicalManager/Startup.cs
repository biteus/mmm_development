using MedicalManager.Areas.Identity.Data;
using MedicalManager.Data;
using MedicalManager.Models;
using MedicalManager.Models.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalManager
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
            services.AddRazorPages();
            services.AddMvc().AddXmlSerializerFormatters();

            services.AddDbContext<MedicalManagerDBContext>(options =>
                   options.UseSqlServer(Configuration.GetConnectionString("MedicalManagerDBContextConnection")));

            //services.AddDefaultIdentity<User>().AddRoles<IdentityRole>().AddEntityFrameworkStores<MedicalManagerDBContext>();

            services.AddIdentity<User, IdentityRole>(config =>
            {
                //config.SignIn.RequireConfirmedEmail = true;
                config.SignIn.RequireConfirmedEmail = false;
            }).AddEntityFrameworkStores<MedicalManagerDBContext>().AddDefaultUI().AddDefaultTokenProviders();

            services.AddScoped<IMedication, Models.Repositories.MedicationRepository>();





            //    services.AddMvc()
            //.AddRazorPagesOptions(options =>
            //{
            //    //options.AllowAreas = true;
            //    options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
            //    options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
            //});

            //    services.ConfigureApplicationCookie(options =>
            //    {
            //        options.LoginPath = $"/Identity/Account/Login";
            //        options.LogoutPath = $"/Identity/Account/Logout";
            //        options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            //    });

            // using Microsoft.AspNetCore.Identity.UI.Services;
            //services.AddSingleton<IEmailSender, EmailSender>();


            //services.ConfigureApplicationCookie(options =>
            //{
            //    // Cookie settings
            //    options.Cookie.HttpOnly = true;
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

            //    options.LoginPath = "/Identity/Account/Login";
            //    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            //    options.SlidingExpiration = true;
            //});


            //services.AddDefaultIdentity<User>().AddEntityFrameworkStores<MedicalManagerDBContext>();
            //services.AddIdentity<User,  IdentityRole>().AddEntityFrameworkStores<MedicalManagerDBContext>();




            //services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<MedicalManagerDBContext>();
            //services.AddDbContext<MedicalManagerDBContext>(options =>
            //options.UseSqlServer(Configuration.GetConnectionString("MedicalManagerDBContextConnection")));
            //services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<MedicalManagerDBContext>().AddDefaultTokenProviders();
            //services.AddDefaultIdentity<User>().AddEntityFrameworkStores<MedicalManagerDBContext>();
            //services.AddIdentity<User, IdentityRole>(config =>
            //{
            //    config.SignIn.RequireConfirmedEmail = true;
            //})
            //    .AddEntityFrameworkStores<MedicalManagerDBContext>()
            //    .AddDefaultTokenProviders();

            //services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<MedicalManagerDBContext>();
            //services.AddIdentity<MedicalManagerUser, IdentityRole>().AddEntityFrameworkStores<MedicalManagerDBContext>().AddDefaultTokenProviders();
            //services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
            //    .AddEntityFrameworkStores<MedicalManagerDBContext>();
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
                //app.UseHsts();
            }

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/Home";
                    await next();
                }
            });


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseHttpsRedirection();
            //app.UseStaticFiles();

            //app.UseRouting();


            //app.UseDefaultFiles();
            //app.UseStaticFiles();

            //app.UseRouting();
            //app.UseAuthentication();
            //app.UseAuthorization();

            //app.UseStaticFiles();
            //app.UseRouting();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
