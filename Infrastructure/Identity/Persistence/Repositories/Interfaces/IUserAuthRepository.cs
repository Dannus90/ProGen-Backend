using System;
using System.Threading.Tasks;
using Core.Domain.DbModels;

namespace Infrastructure.Identity.Repositories.Interfaces
{
    public interface IUserAuthRepository
    {
        Task RegisterUser(string password, string email);
        Task SaveRefreshToken(string refreshToken, Guid userId);
        Task<RefreshToken> GetRefreshTokenByUserId(string userId);
        Task DeleteRefreshTokenByUserId(string userId);
        Task UpdateLastLoggedIn(Guid userId);
    }
}