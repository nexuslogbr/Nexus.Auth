using Nexus.Auth.Api.Helpers;
using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Repository.Handlers;
using Nexus.Auth.Repository.Handlers.Interfaces;
using Nexus.Auth.Repository.Services;
using Nexus.Auth.Repository.Services.Interfaces;
using Polly;

namespace Nexus.Auth.API.Dependency
{
    public static class DependencyRegister
    {
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IAccessDataService, AccessDataService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .AllowSelfSignedCertificate()
                .AddPolicyHandler(PollyExtensions.WaitAndTry())
                .AddTransientHttpErrorPolicy(
                    p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddTransient<IAuthHandler, AuthHandler>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ISmtpMailService, SmtpMailService>();
            services.AddScoped<IRoleHandler, RoleHandler>();
            services.AddScoped<IRoleService<Role>, RoleService>();
            services.AddScoped<IUserHandler, UserHandler>();
            services.AddScoped<IUserService<User>, UserService>();
            services.AddScoped<IMenuHandler, MenuHandler>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IPlaceHandler, PlaceHandler>();  
            services.AddScoped<IPlaceService, PlaceService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
        }
    }
}
