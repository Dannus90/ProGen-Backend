using API.helpers.Cloudinary;
using API.helpers.Cloudinary.Interfaces;
using Infrastructure.Business.Services.Interfaces;
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
        public static void AddDependencyInjectionServices(IServiceCollection services)
        {
            services.AddScoped<IUserAuthService, UserAuthService>();
            services.AddScoped<IUserDataService, UserDataService>();
            services.AddScoped<IUserPresentationService, UserPresentationService>();

            // Helpers
            services.AddScoped<ICloudinaryHelper, CloudinaryHelper>();
        }

        public static void AddDependencyInjectionRepositories(IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IUserAuthRepository>(new UserAuthRepository(connectionString));
            services.AddSingleton<IUserRepository>(new UserRepository(connectionString));
            services.AddSingleton<IUserDataRepository>(new UserDataRepository(connectionString));
            services.AddSingleton<IUserPresentationRepository>(new UserPresentationRepository(connectionString));
        }

        public void AddDependencyInjectionHandlers(IServiceCollection services)
        {
            services.AddScoped<ITokenHandler, TokenHandler>();
        }
    }
}