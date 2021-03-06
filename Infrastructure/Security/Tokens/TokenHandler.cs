using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Application.Exceptions;
using Core.Domain.DbModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Infrastructure.Security.Tokens
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IOptions<TokenConfig> _tokenConfig;
        private JwtSecurityTokenHandler _tokenHandler; 

        public TokenHandler(IOptions<TokenConfig> tokenConfig)
        {
            _tokenConfig = tokenConfig;
            _tokenHandler = new JwtSecurityTokenHandler();
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
        
        public string GenerateResetPasswordToken(string email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfig.Value.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, email)
            };

            var token = new JwtSecurityToken(_tokenConfig.Value.Issuer,
                _tokenConfig.Value.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(_tokenConfig.Value.ResetPasswordTokenExpiration),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }

        public bool ValidateJwtToken(string token)
        {
            try
            {
                _tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _tokenConfig.Value.Issuer,
                    ValidAudience = _tokenConfig.Value.Audience,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                            (_tokenConfig.Value.SecretKey))
                }, out _);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public JwtSecurityToken DecodeToken(string token)
        {
            return _tokenHandler.ReadJwtToken(token);
        }

        public string GetUserIdFromAccessToken(string accessToken)
        {
            if (accessToken == null)
            {
                throw new HttpExceptionResponse(401, "No accessToken Provided");
            }

            var stream = accessToken;
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(stream);
                var jwt = jsonToken as JwtSecurityToken;

                return jwt.Subject;
        }
    }
}