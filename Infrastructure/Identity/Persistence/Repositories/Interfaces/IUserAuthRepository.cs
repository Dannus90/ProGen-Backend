using System;
using System.Threading.Tasks;
using Core.Domain.DbModels;
using Core.Domain.Models;

namespace Infrastructure.Identity.Repositories.Interfaces
{
    public interface IUserAuthRepository
    {
        Task RegisterUser(UserCredentialsWithName userCredentialsWithName);
        Task SaveRefreshToken(string refreshToken, Guid userId);
        Task UpdateRefreshTokenByUserId(string refreshToken, Guid userId);
        Task<RefreshToken> GetRefreshTokenByUserId(string userId);
        Task DeleteRefreshTokenByUserId(string userId);
        Task UpdateLastLoggedIn(Guid userId);
        Task UpdatePassword(string newPassword, string userId);
    }
}