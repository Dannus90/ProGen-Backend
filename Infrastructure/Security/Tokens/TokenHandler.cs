using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Application.Exceptions;
using Core.Domain.DbModels;
using Infrastructure.configurations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Infrastructure.Security.Tokens
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IOptions<TokenConfig> _tokenConfig;
        private readonly IOptions<CloudinaryConfig> _cloudinaryConfig;

        public TokenHandler(IOptions<TokenConfig> tokenConfig, IOptions<CloudinaryConfig> cloudinaryConfig)
        {
            _tokenConfig = tokenConfig;
            _cloudinaryConfig = cloudinaryConfig;
        }

        /**
         * Generates a JSON web token.
         *
         * @param {User} userInfo - Contains user data. 
         * 
         * @returns string representing a Json web token.
         */
        public string GenerateJsonWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfig.Value.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email)
            };

            var token = new JwtSecurityToken(_tokenConfig.Value.Issuer,
                _tokenConfig.Value.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(_tokenConfig.Value.AccessTokenExpiration),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }

        /**
         * Generates a refresh token.
         *
         * @param {User} userInfo - Contains user data. 
         * 
         * @returns string representing a Json web token used as a refresh token.
         */
        public string GenerateRefreshToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfig.Value.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email)
            };

            var token = new JwtSecurityToken(_tokenConfig.Value.Issuer,
                _tokenConfig.Value.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(_tokenConfig.Value.RefreshTokenExpiration),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }

        public string GetUserIdFromAccessToken(string accessToken)
        {
            if (accessToken == null)
            {
                throw new HttpExceptionResponse(401, "No accesssToken Provided");
            }

            var stream = accessToken;
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(stream);
                var jwt = jsonToken as JwtSecurityToken;

                return jwt.Subject;
        }
    }
}