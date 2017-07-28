using A4CoreBlog.Data;
using A4CoreBlog.Data.Infrastructure;
using A4CoreBlog.Data.Infrastructure.Mapping;
using A4CoreBlog.Data.Models;
using A4CoreBlog.Data.Seed;
using A4CoreBlog.Data.Services.Contracts;
using A4CoreBlog.Data.Services.Implementations;
using A4CoreBlog.Data.UnitOfWork;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using System.Text;
using System.Threading.Tasks;

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

            // Adds services required for using options.
            services.AddOptions();

            // Register the IConfiguration instance which MyOptions binds against.
            services.Configure<AppConfiguration>(Configuration.GetSection("AppConfiguration"));

            services.AddTransient<BlogSystemSeedData>();

            // Add framework services.
            services.AddSingleton(Configuration);
            services.AddDbContext<BlogSystemContext>();

            services.AddScoped<IBlogSystemContext, BlogSystemContext>();
            services.AddScoped<IBlogSystemData, BlogSystemData>();

            // internal services
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISystemImageService, SystemImageService>();
            services.AddScoped<IAuthService, AuthService>();

            ConfigureServicesIdentity(services);

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
            loggerFactory
                .AddFile("Logs/mylog-{Date}.txt")
                .AddDebug();

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

            app.UseStaticFiles();

            ConfigureSecurity(app);

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

        private static void ConfigureServicesIdentity(IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(config =>
                {
                    config.User.RequireUniqueEmail = true;
                    config.Cookies.ApplicationCookie.LoginPath = "/auth/login";
                })
            .AddEntityFrameworkStores<BlogSystemContext>()
            .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(config =>
            {
                config.Cookies.ApplicationCookie.Events =
                    new CookieAuthenticationEvents
                    {
                        OnRedirectToLogin = ctx =>
                        {
                            // If we were redirect to login from api..
                            if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                            {
                                ctx.Response.StatusCode = 401;
                                return Task.FromResult<object>(null);
                            }

                            ctx.Response.Redirect(ctx.RedirectUri);
                            return Task.FromResult<object>(null);
                        },
                        OnRedirectToAccessDenied = ctx =>
                        {
                            // If access is denied from api...
                            if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                            {
                                ctx.Response.StatusCode = 403;
                                return Task.FromResult<object>(null);
                            }

                            ctx.Response.Redirect(ctx.RedirectUri);
                            return Task.FromResult<object>(null);
                        }
                    };
            });
        }

        private void ConfigureSecurity(IApplicationBuilder app)
        {
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("AppConfiguration:Key").Value)),
                    ValidAudience = Configuration.GetSection("AppConfiguration:SiteUrl").Value,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = Configuration.GetSection("AppConfiguration:SiteUrl").Value
                }
            });

            app.UseIdentity();
        }
    }
}
