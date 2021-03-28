using System;
using System.Data;
using System.Threading.Tasks;
using Core.Domain.DbModels;
using Dapper;
using Infrastructure.Identity.Repositories.Interfaces;
using Npgsql;

namespace Infrastructure.Identity.Repositories
{
    public class UserAuthRepository :IUserAuthRepository
    {
        private readonly string _connectionString;

        public UserAuthRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private static IDbConnection connectDb(string connectionString)
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.OpenAsync();
            return connection;
        }

        public async Task RegisterUser(string hashedPassword, string email)
        {
            const string query = @"
                    Insert into user_base (id, email, password)
                    VALUES (@Id, @Email, @Password);  
                ";
            
            var userId = Guid.NewGuid();

            using var conn = connectDb(_connectionString);
            
            await conn.ExecuteScalarAsync(query, new
            {
                Email = email,
                Password = hashedPassword,
                Id = userId
            });
        }
        
        public async Task SaveRefreshToken(string refreshToken, Guid userId)
        {
            const string query = @"
                    Insert into refresh_token (id, user_id, refresh_token)
                    VALUES (@TokenId, @UserId, @RefreshToken);  
                ";
            
            var tokenId = Guid.NewGuid();
            
            using var conn = connectDb(_connectionString);
            
            await conn.ExecuteScalarAsync(query, new
            {
                RefreshToken = refreshToken,
                UserId = userId,
                TokenId = tokenId
            });
        }
        
        public async Task<RefreshToken> GetRefreshTokenByUserId(string userId)
        {
            const string query = @"
                    SELECT id AS IdString,
                            user_Id AS UserIdString,
                            refresh_token AS RefreshToken,
                            token_set_at AS TokenSetAt
                    FROM refresh_token WHERE user_id = @UserId;  
                ";
            
            using var conn = connectDb(_connectionString);
            
            return await conn.QueryFirstOrDefaultAsync<RefreshToken>(query, new
            {
                UserId = userId
            });
        }
    }
}