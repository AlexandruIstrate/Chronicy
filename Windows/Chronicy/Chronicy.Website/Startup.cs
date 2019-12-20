using System;
using System.IO;
using Chronicy.Sql;
using Chronicy.Data.Activity;
using Chronicy.Data.Automation;
using Chronicy.Data.Statistics;
using Chronicy.Website.Identity;
using Chronicy.Website.Stores;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Chronicy.Website.Services;
using System.Net;
using Chronicy.Config;
using Settings = Chronicy.Website.Config.Settings;

namespace Chronicy.Website
{
    public class Startup
    {
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
            })
            .AddMvc();

            // Add identity types
            services.AddIdentity<ChronicyUser, ChronicyRole>(config =>
            {
#if DEBUG
                config.SignIn.RequireConfirmedEmail = false;
#else
                config.SignIn.RequireConfirmedEmail = true;
#endif
            })
            .AddDefaultTokenProviders();

            // Add database
            services.AddTransient<ISqlDatabase>(e => new SqlServerDatabase(SqlConnectionFactory.Create(Configuration)));

            // Add automation services
            services.AddTransient<IActivityManager, ActivityManager>();
            services.AddTransient<IAutomationManager, AutomationManager>();
            services.AddTransient<IStatisticsManager, StatisticsManager>();

            // Identity Services
            services.AddTransient<IUserStore<ChronicyUser>, UserStore>();
            services.AddTransient<IRoleStore<ChronicyRole>, RoleStore>();

            services.AddTransient<IEmailSender, EmailSender>(e => new EmailSender(Configuration.GetValue<string>(Settings.Email.Host),
                new NetworkCredential
                {
                    UserName = Configuration.GetValue<string>(Settings.Email.Username),
                    Password = Configuration.GetValue<string>(Settings.Email.Password)
                }));
            services.AddTransient<IEmailBuilder, ConfirmationEmailBuilder>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;

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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.EnvironmentName == Environments.Development)
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
            builder.AddJsonFile(ConfigFiles.MainSettings, optional: false, reloadOnChange: true);
            builder.AddJsonFile(ConfigFiles.DevelopmentSettings, optional: true);
            builder.AddJsonFile(ConfigFiles.DatabaseSettings, optional: true);
            builder.AddEnvironmentVariables();
            return builder.Build();
        }
    }
}
