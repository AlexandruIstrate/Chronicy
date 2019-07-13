using Chronicy.Sql;
using Chronicy.Web.Api;
using Chronicy.Web.Auth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data.SqlClient;
using System.Reflection;

namespace Chronicy.Web
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
            SqlServerDatabase database = new SqlServerDatabase(CreateConnection());

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddTransient<IAuthentication>(e => new AuthenticationApi(database));
            services.AddTransient<INotebook>(e => new NotebookApi(new SqlDataSource(database)));
            services.AddTransient<ITokenManager>(e => new TokenManager(database));
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private SqlConnection CreateConnection()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                ApplicationName = Assembly.GetExecutingAssembly().GetName().Name
            };

            return new SqlConnection(builder.ToString());
        }
    }
}
