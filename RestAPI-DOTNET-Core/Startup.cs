using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestAPI_DOTNET_Core.Data;

namespace RestAPI_DOTNET_Core
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
            services.AddControllers();
            //specific to API- passing connection string
            //hardcoded here
            //need to explore a new way to pass through appsetting
            //how to secure the connection string
            //services.AddDbContext<QuoteDbContext>(option => option.UseSqlServer(@"Data Source=localhost;Initial Catalog=QuotesDb; User ID=sa;Password=Srikanth@123"));
            services.AddDbContext<QuoteDbContext>(option => option.UseSqlServer(Configuration.GetConnectionString("QuoteDbContext")));
            //ability to pass XML data back to client
            //default value is JSON
            //.AddMvc().AddXmlSerializerFormatters();
            //services.AddControllers(options =>
            //{
            //    options.RespectBrowserAcceptHeader = true; // false by default
            //});
            services.AddMvc().AddXmlSerializerFormatters().AddXmlDataContractSerializerFormatters();
            services.AddResponseCaching();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = "https://srisai1986.us.auth0.com/";
                options.Audience = "https://localhost:5001/";
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, QuoteDbContext quoteDbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            //app.UseMvc();
            app.UseAuthentication();
            app.UseRouting();
            
            app.UseAuthorization();
            app.UseResponseCaching();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //specific to API
            //only useful schema will not change
            quoteDbContext.Database.EnsureCreated();

            //update migration @ runtime
            //quoteDbContext.Database.Migrate();
            

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //      name: "default",
            //      template: "{controller=Home}/{action=Index}/{id?}");
            //});
        }
    }
}


//docker container run -d -p 1433:1433 \
//--volume mssqlsystem:/var/opt/mssql \
//--volume mssqluser:/var/opt/sqlserver \
//--env ACCEPT_EULA = Y \
//--env SA_PASSWORD = Srikanth@123 \
//--name ms-sql-server\
//mcr.microsoft.com / mssql / server:2019 - latest


//docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Srikanth@123" -p 1433:1433 --name mssql_docker -d mcr.microsoft.com/mssql/server:2019-latest

//docker exec -it mssql_docker bash



//EF core specific


//add migration
//dotnet ef migrations add QuotesAdded

//update
//dotnet ef database update

//remove migration - dotnet ef migrations remove
//dotnet ef migrations remove