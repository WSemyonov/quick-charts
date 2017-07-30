using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using QuickCharts.Models.Database;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace QuickCharts
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile(
                    path: "appsettings.json",
                    optional: false,
                    reloadOnChange: true
                );

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            switch (Configuration["database"])
            {
                case "sqlite":
                    var directorySqlite = $"{ Directory.GetCurrentDirectory() }\\AppData";

                    if ( !Directory.Exists(directorySqlite) )
                        Directory.CreateDirectory(directorySqlite);

                    var connection = $"{directorySqlite}\\{Configuration["connection-string:sqlite"]}";

                    services.AddDbContext<DatabaseContext>(
                        optionsAction: (options) => options.UseSqlite(connection)
                    );
                    break;

                case "mssql":
                    throw new Exception("for mssql not defined connection string");

                case "postgresql":
                    throw new Exception("for postgresql not defined connection string");

                default:
                    throw new Exception("not implemented the database connection");
            }

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // app.Run(async (context) =>
            // {
            //     await context.Response.WriteAsync("Hello World!");
            // });
        }
    }
}
