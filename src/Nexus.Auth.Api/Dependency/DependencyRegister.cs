﻿using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Repository.Handlers;
using Nexus.Auth.Repository.Handlers.Interfaces;
using Nexus.Auth.Repository.Interfaces;
using Nexus.Auth.Repository.Services;
using Nexus.Auth.Repository.Services.Interfaces;
using Nexus.Repository.Services;

namespace Nexus.Auth.API.Dependency
{
    public static class DependencyRegister
    {
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IAuthHandler, AuthHandler>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddScoped<IRoleHandler<Role>, RoleHandler>();
            services.AddScoped<IRoleService<Role>, RoleService>();
            services.AddScoped<IUserHandler<User>, UserHandler>();
            services.AddScoped<IUserService<User>, UserService>();
            services.AddScoped<IMenuHandler<Menu>, MenuHandler>();
            services.AddScoped<IMenuService<Menu>, MenuService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IManufacturerService, ManufacturerService>();
            services.AddScoped<IModelService, ModelService>();
            services.AddScoped<IVpcItemService, VpcItemService>();
            services.AddScoped<IRequesterService, RequesterService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAccessDataService, AccessDataService>();
        }
    }
}
