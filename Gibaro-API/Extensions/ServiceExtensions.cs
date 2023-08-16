

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Repository.Context;
using Service.Contracts;
using Service;
using Contracts;
using Repository;
using LoggerService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Gibaro_API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
               services.AddCors(options =>
               {
                   options.AddPolicy("CorsPolicy", builder =>
                   builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .WithExposedHeaders("X-Pagination"));
               });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
        services.Configure<IISOptions>(options =>
        {

        });

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<RepositoryContext>(opts =>
          opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 10;
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<RepositoryContext>()
            .AddDefaultTokenProviders()
            .AddUserStore<UserStore<User, Role, RepositoryContext, Guid>>()
            .AddRoleStore<RoleStore<Role, RepositoryContext, Guid>>();
        }

        public static void ConfigureServiceManager(this IServiceCollection services) =>
           services.AddScoped<IServiceManager, ServiceManager>();

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
           services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureLoggerService(this IServiceCollection services) =>
           services.AddSingleton<ILoggerManager, LoggerManager>();

        public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfiguration = new JwtConfiguration();
            configuration.Bind(jwtConfiguration.Section, jwtConfiguration);

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfiguration.ValidIssuer,
                    ValidAudience = jwtConfiguration.ValidAudience,
                };

                if (jwtConfiguration.SecretKey != null)
                    options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.SecretKey));
            });
        }

        public static void AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration) => services.Configure<JwtConfiguration>(configuration.GetSection("JwtSettings"));

    }
}
