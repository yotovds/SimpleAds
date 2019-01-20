using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleAds.Data.Models;
using SimpleAds.Data;
using SimpleAds.Services.ViewModels.Ads;
using SimpleAds.Services;
using SimpleAds.Services.Contracs;
using GlobalConstans;

namespace SimpleAds.Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<SimpleAdsDbContext>(options =>
                    options.UseSqlServer(
                        this.Configuration.GetConnectionString(StringConstants.ConnectioString)));

            services.AddIdentity<SimpleAdsUser, IdentityRole>()
                .AddEntityFrameworkStores<SimpleAdsDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });

            services.AddAntiforgery();

            services.AddAutoMapper(configuration =>
            {
                configuration.CreateMap<CreateAdInputModel, Ad>();
                configuration.CreateMap<Ad, AdViewModel>();
                configuration.CreateMap<AdViewModel, CreateAdInputModel>();
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = StringConstants.DefaultLoginPath;
            });

            services.AddScoped<IAdsService, AdsService>();
            services.AddScoped<ISearchService, SearchService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            CreateUserRoles(services).GetAwaiter().GetResult();
        }

        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<SimpleAdsUser>>();

            if (roleManager.Roles.Any() == false)
            {
                //create the roles and seed them to the database
                await roleManager.CreateAsync(new IdentityRole(StringConstants.AdminRole));
                await roleManager.CreateAsync(new IdentityRole(StringConstants.UserRole));
                var admin = new SimpleAdsUser { UserName = "admin"};
                await userManager.CreateAsync(admin, "admin");
                await userManager.AddToRoleAsync(admin, StringConstants.AdminRole);
            }
        }
    }
}
