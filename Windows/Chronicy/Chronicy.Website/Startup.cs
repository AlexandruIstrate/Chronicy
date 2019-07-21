using System;
using System.IO;
using Chronicy.Sql;
using Chronicy.Standard.Data.Activity;
using Chronicy.Standard.Data.Automation;
using Chronicy.Standard.Data.Statistics;
using Chronicy.Website.Identity;
using Chronicy.Website.Services;
using Chronicy.Website.Stores;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Chronicy.Website
{
    public class Startup
    {
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        public Startup()
        {
            Configuration = CreateConfiguration();
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

            // Add identity types
            services.AddIdentity<ChronicyUser, ChronicyRole>(config =>
            {
                //config.SignIn.RequireConfirmedEmail = true;

                // Debug only
                config.SignIn.RequireConfirmedEmail = false;
            })
                .AddDefaultTokenProviders()
                .AddDefaultUI(UIFramework.Bootstrap4);

            // Add database
            services.AddTransient<ISqlDatabase>(e => new SqlServerDatabase(SqlConnectionFactory.Create(Configuration)));

            // Add automation services
            services.AddTransient<IActivityManager, ActivityManager>();
            services.AddTransient<IAutomationManager, AutomationManager>();
            services.AddTransient<IStatisticsManager, StatisticsManager>();

            // Identity Services
            services.AddTransient<IUserStore<ChronicyUser>, UserStore>();
            services.AddTransient<IRoleStore<ChronicyRole>, RoleStore>();
            services.AddTransient<IEmailSender, EmailSender>();

            services.Configure<IdentityOptions>(options =>
            {
                // TODO: Change these settings to something reasonable
                // Password settings
                //options.Password.RequireDigit = true;
                //options.Password.RequireLowercase = true;
                //options.Password.RequireNonAlphanumeric = true;
                //options.Password.RequireUppercase = true;
                //options.Password.RequiredLength = 8;
                //options.Password.RequiredUniqueChars = 1;

                // Debug only options
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = WebsitePaths.Login;
                options.AccessDeniedPath = WebsitePaths.AccessDenied;
                options.SlidingExpiration = true;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(WebsitePaths.Error);
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc();
        }

        private IConfiguration CreateConfiguration()
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            builder.AddJsonFile($"appsettings.{ Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production" }.json", optional: true);
            builder.AddJsonFile("appsettings.Database.json", optional: true);
            builder.AddEnvironmentVariables();
            return builder.Build();
        }
    }
}
