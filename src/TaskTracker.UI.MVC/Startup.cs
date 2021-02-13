using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using TaskTracker.Infrastructure.Data;
using TaskTracker.Infrastructure.IoC;
using TaskTracker.UI.MVC.Interfaces;
using TaskTracker.UI.MVC.Services;

namespace TaskTracker.UI.MVC
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
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookies")
            .AddOpenIdConnect("oidc", options =>
            {
                options.Authority = "https://localhost:6004/";

                options.ClientId = "tasktracker.mvc";
                options.ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";
                options.ResponseType = "code";
                options.GetClaimsFromUserInfoEndpoint = true;
                options.ClaimActions.MapJsonKey("role", "role", "role");
                options.ClaimActions.MapJsonKey("UserId", "UserId", "UserId");
                options.TokenValidationParameters.NameClaimType = "name";
                options.TokenValidationParameters.RoleClaimType = "role";

                options.SaveTokens = true;
            });

            services.AddDbContext<TaskTrackerContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("TaskTrackerConnection")));

            services.AddAutoMapper(typeof(Startup));

            services.AddTransient<IFileService, FileService>();
            services.AddTransient<ITaskService, TaskService>();

            RegisterServices(services, Configuration.GetConnectionString("ProviderType"));

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

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
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute()
                    .RequireAuthorization();
            });
        }
        private static void RegisterServices(IServiceCollection services, string mode)
        {
            DependencyContainer.RegisterServices(services);
            DependencyContainer.RegisterProvider(services, mode);
        }
    }
}