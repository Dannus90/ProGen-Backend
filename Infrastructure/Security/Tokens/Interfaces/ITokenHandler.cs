using Core.Domain.DbModels;

namespace Infrastructure.Security.Tokens
{
    public interface ITokenHandler
    {
        string GenerateJsonWebToken(User userInfo);
        string GenerateRefreshToken(User userInfo);
        string GetUserIdFromAccessToken(string accessToken);
        string GenerateResetPasswordToken(string email);
    }
}