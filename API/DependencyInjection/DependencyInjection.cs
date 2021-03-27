using System;
using Infrastructure.Identity.Repositories;
using Infrastructure.Identity.Repositories.Interfaces;
using Infrastructure.Identity.Services;
using Infrastructure.Identity.Services.Interfaces;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Repositories.Interfaces;
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
    }
}