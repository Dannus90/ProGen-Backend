using System;
using Infrastructure.Identity.Repositories;
using Infrastructure.Identity.Repositories.Interfaces;
using Infrastructure.Identity.Services;
using Infrastructure.Identity.Services.Interfaces;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Repositories.Interfaces;
using Infrastructure.Security.Tokens;
using Microsoft.Extensions.DependencyInjection;

namespace API
{
    public class DependencyInjection
    {
        public void AddDependencyInjectionServices(IServiceCollection services)
        {
            services.AddScoped<IUserAuthService, UserAuthService>();
        }
        
        public void AddDependencyInjectionRepositories(IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IUserAuthRepository>(new UserAuthRepository(connectionString));
            services.AddSingleton<IUserRepository>(new UserRepository(connectionString));
        }

        public void AddDependencyInjectionHandlers(IServiceCollection services)
        {
            services.AddScoped<ITokenHandler, TokenHandler>();
        }
    }
}