using Chronicy.Sql;
using Chronicy.Standard.Data;
using Chronicy.Web.Api;
using Chronicy.Web.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text.Json;

namespace Chronicy.Web
{
    public class Startup
    {
        private SqlServerDatabase database;

        public Startup()
        {
            Configuration = CreateConfiguration();

            ConfigureDatabase();
            ConfigureJson();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
                {
                    options.EnableEndpointRouting = false;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
                });

            services.AddTransient<IAuthentication>(e => new AuthenticationApi(database));
            services.AddTransient<INotebook>(e => new NotebookApi(new SqlDataSource(database)));
            services.AddTransient<ITokenManager>(e => new TokenManager(database));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            applicationLifetime.ApplicationStopping.Register(OnShutdown);

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private void OnShutdown()
        {
            database.Dispose();
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

        private void ConfigureJson()
        {
            JsonConvert.DefaultSettings = () => JsonDefaultSettings.SerializerSettings;
        }

        private void ConfigureDatabase()
        {
            database = new SqlServerDatabase(SqlConnectionFactory.Create(Configuration));
        }
    }
}