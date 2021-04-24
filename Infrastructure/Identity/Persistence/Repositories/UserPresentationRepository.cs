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
    public class UserPresentationRepository : IUserPresentationRepository
    {
        private readonly string _connectionString;

        public UserPresentationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<UserPresentation> CreateUserPresentation(string userId, UserPresentation userPresentation)
        {
            const string queryUserPresentation = @"
                    Insert into user_presentation (id, user_id, presentation_sv, presentation_en)
                    VALUES (@Id, @UserId, @PresentationSv, @PresentationEn);

                    SELECT * FROM user_presentation
                    WHERE user_id = @UserId;
                ";
            
            var id = Guid.NewGuid();
            
            using var conn = await connectDb(_connectionString);
            
            return await conn.ExecuteScalarAsync<UserPresentation>(queryUserPresentation, new
            {
                userPresentation.PresentationSv,
                userPresentation.PresentationEn,
                UserId = userId,
                Id = id
            });
        }
        
        public async Task<UserPresentation> GetUserPresentation(string userId)
        {
            const string queryUserPresentation = @"
                    SELECT * FROM user_presentation
                    WHERE user_id = @UserId;
                ";
            
            using var conn = await connectDb(_connectionString);
            
            return await conn.ExecuteScalarAsync<UserPresentation>(queryUserPresentation, new
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