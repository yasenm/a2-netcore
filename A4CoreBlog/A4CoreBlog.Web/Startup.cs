using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using A4CoreBlog.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using A4CoreBlog.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using A4CoreBlog.Data.Infrastructure.Mapping;
using A4CoreBlog.Data.Seed;
using A4CoreBlog.Data.UnitOfWork;
using A4CoreBlog.Data.Services.Implementations;
using A4CoreBlog.Data.Services.Contracts;

namespace A4CoreBlog_Web
{
    public class Startup
    {
        private IHostingEnvironment _env;

        public Startup(IHostingEnvironment env)
        {
            _env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Automapper configuration initialization
            AutoMapperConfig.Init();
            
            services.AddTransient<BlogSystemSeedData>();

            // Add framework services.
            services.AddSingleton(Configuration);
            services.AddDbContext<BlogSystemContext>();

            services.AddScoped<IBlogSystemContext, BlogSystemContext>();
            services.AddScoped<IBlogSystemData, BlogSystemData>();

            // internal services
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IPostService, PostService>();

            services.AddIdentity<User, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Cookies.ApplicationCookie.LoginPath = "/auth/login";
            })
            .AddEntityFrameworkStores<BlogSystemContext>();

            // Add framework services.
            services.AddMvc(config =>
            {
                if (_env.IsProduction())
                {
                    config.Filters.Add(new RequireHttpsAttribute());
                }
            })
            .AddJsonOptions(config =>
            {
                config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, BlogSystemSeedData seeder)
        {
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseIdentity();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                      name: "areaRoute",
                      template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });

            seeder.SeedData().Wait();
        }
    }
}
