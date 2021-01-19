using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PeaLearning.Application.Services;
using PeaLearning.Common;
using PeaLearning.Common.Utility;
using PeaLearning.Domain.AggregateModels.UserAggregate;
using PeaLearning.Infrastructure;
using PeaLearning.Website.Helpers;
using Shared.Constants;
using Shared.EF;
using Shared.Infrastructure;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text.Json;

namespace PeaLearning.Website
{
    public class Startup
    {
        public static readonly string AssemblyName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            AppSettings.Instance.SetConfiguration(configuration);

            try
            {
                Console.WriteLine($"Hello: {DateTime.Now}");
                StaticVariable.StaticVersion = Environment.GetEnvironmentVariable("UUID");
            }
            catch
            {
                StaticVariable.StaticVersion = DateTime.Now.Ticks.GetHashCode().ToString();
            }

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services
                .AddDbContext<PeaDbContext>(options =>
                {
                    options.UseLazyLoadingProxies()
                        .UseSqlServer(Configuration.GetConnectionString(ConfigurationKeys.DefaultConnectionString),
                            b => { b.MigrationsAssembly(AssemblyName); })
                        .UseSnakeCaseNamingConvention().EnableSensitiveDataLogging();
                })
                .AddScoped<IDbConnection>(sp =>
                {
                    var config = sp.GetRequiredService<IConfiguration>();
                    var connection =
                        new SqlConnection(config.GetConnectionString(ConfigurationKeys.DefaultConnectionString));
                    connection.Open();
                    return connection;
                })
                .AddMediatREvent()
                .AddIdentity()
                .AddRepositories()
                .AddServices();
            services.ConfigureApplicationCookie(options => options.LoginPath = "/dang-nhap");
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(2);
                options.Cookie.HttpOnly = true;
            });
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            }).AddNewtonsoftJson();
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            services
                .AddRouting(options => options.LowercaseUrls = true)
                .AddControllers();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddScoped<IUserClaimsPrincipalFactory<User>, CustomClaimsPrincipalFactory>();
            services.AddScoped<IUserClaim, UserClaim>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUnitOfWork, UnitOfWork<PeaDbContext>>();
            services.Configure<UrlEndpoints>(Configuration.GetSection(ConfigurationKeys.UrlEndpoints));
            services.AddHttpClient<IFileService, FileService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/error-{Date}.txt");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
