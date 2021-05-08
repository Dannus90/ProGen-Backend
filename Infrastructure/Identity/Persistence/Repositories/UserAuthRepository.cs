using System;
using System.Data;
using System.Threading.Tasks;
using Core.Domain.DbModels;
using Core.Domain.Models;
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

        public async Task RegisterUser(UserCredentialsWithName userCredentialsWithName)
        {
            const string queryUserBase = @"
                    Insert into user_base (id, email, password, first_name, last_name)
                    VALUES (@Id, @Email, @Password, @FirstName, @LastName);  
                ";
            
            const string queryUserData = @"
                    Insert into user_data (id, user_id, email_cv)
                    VALUES (@Id, @UserId, @Email);  
                ";
            
            const string queryUserPresentation = @"
                    Insert into user_presentation (id, user_id, presentation_sv, presentation_en)
                    VALUES (@Id, @UserId, @PresentationSv, @PresentationEn);
                ";

            const string queryOtherInformation = @"
                    Insert into other_information (id, user_id, driving_license_sv, driving_license_en)
                    VALUES (@Id, @UserId, @DrivingLicenseSv, @DrivingLicenseEn);
                ";
            
            using var conn = await connectDb(_connectionString);
            
            // Begin transaction.
            using var transaction = conn.BeginTransaction();
            
            var userId = Guid.NewGuid();
                
            await conn.ExecuteScalarAsync(queryUserBase, new
            {
                userCredentialsWithName.Email,
                userCredentialsWithName.Password,
                userCredentialsWithName.FirstName,
                userCredentialsWithName.LastName,
                Id = userId
            });
            
            var userDataId = Guid.NewGuid();
            
            await conn.ExecuteScalarAsync(queryUserData, new
            {
                Id = userDataId,
                UserId = userId,
                userCredentialsWithName.Email,
            });
            
            var userPresentationId = Guid.NewGuid();
            
            await conn.ExecuteScalarAsync(queryUserPresentation, new
            {
                PresentationSv = "",
                PresentationEn = "",
                UserId = userId,
                Id = userPresentationId
            });
            
            var otherInformationId = Guid.NewGuid();
            
            await conn.ExecuteScalarAsync(queryOtherInformation, new
            {
                DrivingLicenseSv = "",
                DrivingLicenseEn = "",
                Id = otherInformationId,
                UserId = userId
            });
                
            transaction.Commit();
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
                            refresh_token AS Token,
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

        public async Task UpdatePassword(string newPassword, string userId)
        {
            const string query = @"
                    UPDATE user_base
                    SET password = @NewPassword
                    WHERE id = @UserId;
                ";

            using var conn = await connectDb(_connectionString);

            await conn.ExecuteScalarAsync(query, new
            {
                NewPassword = newPassword,
                UserId = userId
            });
        }

        public async Task UpdateEmail(string email, string userId)
        {
            const string query = @"
                    UPDATE user_base
                    SET email = @Email
                    WHERE id = @UserId;
                ";

            using var conn = await connectDb(_connectionString);

            await conn.ExecuteScalarAsync(query, new
            {
                Email = email,
                UserId = userId
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