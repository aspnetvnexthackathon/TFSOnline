using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.Security.Cookies;
using Microsoft.Data.Entity;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using TFSOnline.Models;

namespace TFSOnline
{
    public class Startup
    {
        public void Configure(IBuilder app)
        {
            var configuration = new Configuration();
            configuration.AddJsonFile("Config.json");
            configuration.AddEnvironmentVariables();

            app.UseServices(services =>
            {
                // Add EF services to the services container
                services.AddEntityFramework()
                        .AddSqlServer();

                services.AddScoped<TFSOnlineContext>();

                // Configure DbContext
                services.SetupOptions<TFSOnlineContextOptions>(options =>
                    options.UseSqlServer(configuration.Get("Data:DefaultConnection:ConnectionString")));

                // Add Identity services to the services container
                services.AddIdentity<ApplicationUser>()
                        .AddEntityFramework<ApplicationUser, TFSOnlineContext>()
                        .AddHttpSignIn();

                // Add MVC services to the services container
                services.AddMvc();

                //Add all SignalR related services to IoC.
                services.AddSignalR();
            });

            /* Error page middleware displays a nice formatted HTML page for any unhandled exceptions in the request pipeline.
             * Note: ErrorPageOptions.ShowAll to be used only at development time. Not recommended for production.
             */
            app.UseErrorPage(ErrorPageOptions.ShowAll);

            //Configure SignalR
            app.UseSignalR();

            // Add static files to the request pipeline
            app.UseStaticFiles();

            // Add cookie-based authentication to the request pipeline
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });

            // Add MVC to the request pipeline
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });

                routes.MapRoute(
                    name: "api",
                    template: "{controller}/{id?}");
            });

            DbInitializer.InitializeTFSOnlineDatabaseAsync(app.ApplicationServices).Wait();
        }
    }
}