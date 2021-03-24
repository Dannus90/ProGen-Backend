using Infrastructure.Identity.Services.Interfaces;

namespace Infrastructure.Identity.Services
{
    public class UserAuthService : IUserAuthService
    {
        public string Test()
        {
            return "DependencyInjectionWorking!";
        }
    }
}