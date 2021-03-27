using System;
using System.Data;
using System.Threading.Tasks;
using Core.Domain.DbModels;
using Dapper;
using Infrastructure.Persistence.Repositories.Interfaces;
using Npgsql;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private static IDbConnection connectDb(string connectionString)
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.OpenAsync();
            return connection;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            const string query = @"
                    SELECT id AS IdString,
                            email AS Email,
                            password AS Password,
                            last_login AS LastLogin,
                            created_at AS CreatedAt,
                            updated_at AS UpdatedAt   
                    FROM user_base WHERE email = @Email;  
                ";
            
            using var conn = connectDb(_connectionString);

            return await conn.QueryFirstOrDefaultAsync<User>(query, new
            {
                Email = email
            });
        }
        
        public async Task UpdateLastLoggedIn(Guid userId)
        {
            const string query = @"
                    UPDATE user_base
                    SET last_login = @CurrentTime
                    WHERE id = @UserId;  
                ";
            
            using var conn = connectDb(_connectionString);

            var currentTime = DateTime.Now;

            await conn.ExecuteScalarAsync(query, new
            {
                UserId = userId.ToString(),
                CurrentTime = currentTime
            });
        }
    }
}