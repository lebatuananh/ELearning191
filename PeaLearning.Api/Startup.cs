using System.Data;
using System.Reflection;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PeaLearning.Api.Helpers;
using PeaLearning.Infrastructure;
using Shared.Constants;
using Shared.EF;
using Shared.Infrastructure;
using Shared.Localization;


namespace PeaLearning.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public static readonly string AssemblyName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
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
                .AddServices()
                .AddAuthentication(Configuration);
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            services
                .AddRouting(options => options.LowercaseUrls = true)
                .AddControllers();
            services.AddMvc().AddJsonOptions(opts =>
            {
                opts.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                opts.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            }).AddNewtonsoftJson();
            services.AddSwagger();
            services.ConfigureLocalization();
            services.ConfigureInitialization();
            services.AddScoped<IUserClaim, UserClaim>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUnitOfWork, UnitOfWork<PeaDbContext>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);
            app.UseCors(opts =>
            {
                opts.AllowAnyHeader();
                opts.AllowAnyMethod();
                opts.AllowCredentials();
                opts.SetIsOriginAllowed(origin => true);
            });
            app.UseSwaggerUi();
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}