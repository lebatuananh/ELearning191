using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PeaLearning.Infrastructure;
using Shared.Infrastructure;
using Shared.Initialization;
using System.Text;
using MediatR;
using System.Reflection;
using PeaLearning.Api.Initializations;
using Microsoft.AspNetCore.Identity;
using PeaLearning.Domain.AggregateModels.UserAggregate;
using System;
using PeaLearning.Domain.AggregateModels.BannerAggregate;
using PeaLearning.Domain.AggregateModels.BlogAggregate;
using PeaLearning.Domain.DomainServices.Impl;
using PeaLearning.Domain.DomainServices.Interfaces;
using PeaLearning.Domain.AggregateModels.CourseAggregate;
using PeaLearning.Domain.AggregateModels.TagAggregate;
using PeaLearning.Infrastructure.Repositories;

namespace PeaLearning.Api.Helpers
{
    public static class StartupHelper
    {
        public static IServiceCollection ConfigureInitialization(this IServiceCollection services)
        {
            services.AddTransient<IInitializationStage, DbMigrationInitializationStage<PeaDbContext>>();
            services.AddTransient<IInitializationStage, AddDefaultDataStage>();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<IBlogRepository, BlogRepository>();
            services.AddTransient<ITagRepository, TagRepository>();
            services.AddTransient<IBannerRepository, BannerRepository>();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            return services;
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddScoped<UserManager<User>, UserManager<User>>();
            services.AddScoped<RoleManager<Role>, RoleManager<Role>>();
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<PeaDbContext>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;

                // User settings
                options.User.RequireUniqueEmail = true;
            });
            return services;
        }

        public static IServiceCollection AddMediatREvent(this IServiceCollection services)
        {
            services
                .AddMediatR(typeof(Application.Write.Assembly).GetTypeInfo().Assembly)
                .AddMediatR(typeof(Application.Read.Assembly).GetTypeInfo().Assembly);
            return services;
        }

        public static IServiceCollection AddAuthentication(this IServiceCollection services,
            IConfiguration Configuration)
        {
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;

                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = Configuration["Tokens:Issuer"],
                    ValidAudience = Configuration["Tokens:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
                };
            });
            return services;
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "PEA Learning API", Version = "v1"});

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                c.AddSecurityDefinition("Bearer", securitySchema);
                var securityRequirement = new OpenApiSecurityRequirement {{securitySchema, new[] {"Bearer"}}};
                c.AddSecurityRequirement(securityRequirement);
            });
        }

        public static void UseSwaggerUi(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PEA Learning API v1");
                c.DocumentTitle = "PEA Learning API";
            });
        }
    }
}