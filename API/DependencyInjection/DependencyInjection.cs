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
            services.AddScoped<IWorkExperienceService, WorkExperienceService>();
            services.AddScoped<IEducationService, EducationService>();
            services.AddScoped<IOtherInformationService, OtherInformationService>();
            services.AddScoped<ILanguageService, LanguageService>();

            // Helpers
            services.AddScoped<ICloudinaryHelper, CloudinaryHelper>();
        }

        public static void AddDependencyInjectionRepositories(IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IUserAuthRepository>(new UserAuthRepository(connectionString));
            services.AddSingleton<IUserRepository>(new UserRepository(connectionString));
            services.AddSingleton<IUserDataRepository>(new UserDataRepository(connectionString));
            services.AddSingleton<IUserPresentationRepository>(new UserPresentationRepository(connectionString));
            services.AddSingleton<IWorkExperienceRepository>(new WorkExperienceRepository(connectionString));
            services.AddSingleton<IEducationRepository>(new EducationRepository(connectionString));
            services.AddSingleton<IOtherInformationRepository>(new OtherInformationRepository(connectionString));
            services.AddSingleton<ILanguageRepository>(new LanguageRepository(connectionString));
        }

        public void AddDependencyInjectionHandlers(IServiceCollection services)
        {
            services.AddScoped<ITokenHandler, TokenHandler>();
        }
    }
}