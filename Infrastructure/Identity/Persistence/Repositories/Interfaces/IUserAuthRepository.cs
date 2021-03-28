using System;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Repositories.Interfaces
{
    public interface IUserAuthRepository
    {
        Task RegisterUser(string password, string email);
        Task SaveRefreshToken(string refreshToken, Guid userId);
    }
}