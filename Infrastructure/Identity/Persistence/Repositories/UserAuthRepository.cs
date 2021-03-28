using System;
using System.Data;
using System.Threading.Tasks;
using Core.Domain.DbModels;
using Dapper;
using Infrastructure.Identity.Repositories.Interfaces;
using Npgsql;

namespace Infrastructure.Identity.Repositories
{
    public class UserAuthRepository : IUserAuthRepository
    {
        private readonly string _connectionString;

        public UserAuthRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task RegisterUser(string hashedPassword, string email)
        {
            const string query = @"
                    Insert into user_base (id, email, password)
                    VALUES (@Id, @Email, @Password);  
                ";

            var userId = Guid.NewGuid();

            using var conn = await connectDb(_connectionString);

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

            using var conn = await connectDb(_connectionString);

            await conn.ExecuteScalarAsync(query, new
            {
                RefreshToken = refreshToken,
                UserId = userId,
                TokenId = tokenId
            });
        }

        public async Task UpdateRefreshTokenByUserId(string refreshToken, Guid userId)
        {
            const string query = @"
                    UPDATE refresh_token
                    SET refresh_token = @RefreshToken
                    WHERE user_id = @UserId;
                ";

            using var conn = await connectDb(_connectionString);

            await conn.ExecuteScalarAsync(query, new
            {
                RefreshToken = refreshToken,
                UserId = userId.ToString()
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

            using var conn = await connectDb(_connectionString);

            return await conn.QueryFirstOrDefaultAsync<RefreshToken>(query, new
            {
                UserId = userId
            });
        }

        public async Task DeleteRefreshTokenByUserId(string userId)
        {
            const string query = @"
                    DELETE FROM refresh_token
                    WHERE refresh_token.user_id = @UserId;
                ";

            using var conn = await connectDb(_connectionString);

            await conn.ExecuteScalarAsync(query, new
            {
                UserId = userId
            });
        }

        public async Task UpdateLastLoggedIn(Guid userId)
        {
            const string query = @"
                    UPDATE user_base
                    SET last_login = @LastLoggedIn
                    WHERE id = @UserId;
                ";

            var lastLogin = DateTime.Now;

            using var conn = await connectDb(_connectionString);

            await conn.ExecuteScalarAsync(query, new
            {
                LastLoggedIn = lastLogin,
                UserId = userId.ToString()
            });
        }

        private static async Task<IDbConnection> connectDb(string connectionString)
        {
            var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}