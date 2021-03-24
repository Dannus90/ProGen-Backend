using System;
using Infrastructure.Identity.Services;
using Infrastructure.Identity.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace API
{
    public class DependencyInjection
    {
        public void AddDependencyInjectionServices(IServiceCollection services)
        {
            services.AddScoped<IUserAuthService, UserAuthService>();
        }
        
        public void AddDependencyInjectionRepositories(IServiceCollection services)
        {
            Console.WriteLine("Hello");
        }
    }
}