using System;
using System.Data;
using System.Threading.Tasks;
using Core.Domain.DbModels;
using Dapper;
using Infrastructure.Identity.Repositories.Interfaces;
using Npgsql;

namespace Infrastructure.Identity.Repositories
{
    public class UserPresentationRepository : IUserPresentationRepository
    {
        private readonly string _connectionString;

        public UserPresentationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<UserPresentation> UpdateUserPresentation(string userId, UserPresentation userPresentation)
        {
            const string queryUserPresentation = @"
                    UPDATE user_presentation 
                    SET presentation_sv = @PresentationSv,
                        presentation_en = @PresentationEn
                    WHERE user_id = @UserId
                ";
            
            const string queryRetrieveUserPresentation = @"
                    SELECT id AS IdString,
                        user_Id AS UserIdString,
                        presentation_sv AS PresentationSv,
                        presentation_en AS PresentationEn,
                        created_at AS CreatedAt,
                        updated_at AS UpdatedAt
                    FROM user_presentation WHERE user_id = @UserId;  
                ";
            
            var id = Guid.NewGuid();
            
            using var conn = await connectDb(_connectionString);
            
            // Begin transaction.
            using var transaction = conn.BeginTransaction();
            
            await conn.ExecuteScalarAsync(queryUserPresentation, new
            {
                userPresentation.PresentationSv,
                userPresentation.PresentationEn,
                UserId = userId,
                Id = id
            });
            
            var retrievedUserPresentation = await conn.QueryFirstOrDefaultAsync<UserPresentation>
            (queryRetrieveUserPresentation, new
            {
                UserId = userId
            });
            
            transaction.Commit();

            return retrievedUserPresentation;
        }
        
        public async Task<UserPresentation> GetUserPresentation(string userId)
        {
            const string queryUserPresentation = @"
                    SELECT id AS IdString,
                        user_Id AS UserIdString,
                        presentation_sv AS PresentationSv,
                        presentation_en AS PresentationEn,
                        created_at AS CreatedAt,
                        updated_at AS UpdatedAt
                    FROM user_presentation WHERE user_id = @UserId;  
                ";
            
            using var conn = await connectDb(_connectionString);
            
            return await conn.QueryFirstOrDefaultAsync<UserPresentation>(queryUserPresentation, new
            {
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