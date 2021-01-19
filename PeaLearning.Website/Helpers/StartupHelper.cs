using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PeaLearning.Domain.AggregateModels.BannerAggregate;
using PeaLearning.Domain.AggregateModels.BlogAggregate;
using PeaLearning.Domain.AggregateModels.CourseAggregate;
using PeaLearning.Domain.AggregateModels.TagAggregate;
using PeaLearning.Domain.AggregateModels.UserAggregate;
using PeaLearning.Domain.DomainServices.Impl;
using PeaLearning.Domain.DomainServices.Interfaces;
using PeaLearning.Infrastructure;
using PeaLearning.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PeaLearning.Website.Helpers
{
    public static class StartupHelper
    {
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
                o.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(cfg =>
            {
                cfg.LoginPath = "/dang-nhap";
                cfg.AccessDeniedPath = "/forbidden";
                cfg.ExpireTimeSpan = TimeSpan.FromHours(24);
            });
            return services;
        }
    }
}
