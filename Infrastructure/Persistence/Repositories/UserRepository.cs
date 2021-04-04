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

        public async Task<User> GetUserByEmail(string email)
        {
            const string query = @"
                    SELECT id AS IdString,
                            email AS Email,
                            password AS Password,
                            firstname AS Firstname,
                            lastname AS Lastname,
                            last_login AS LastLogin,
                            created_at AS CreatedAt,
                            updated_at AS UpdatedAt   
                    FROM user_base WHERE email = @Email;  
                ";

            using var conn = await connectDb(_connectionString);

            return await conn.QueryFirstOrDefaultAsync<User>(query, new
            {
                Email = email
            });
        }

        public async Task<User> GetUserByUserId(string userId)
        {
            const string query = @"
                    SELECT id AS IdString,
                            email AS Email,
                            password AS Password,
                            last_login AS LastLogin,
                            created_at AS CreatedAt,
                            updated_at AS UpdatedAt   
                    FROM user_base WHERE id = @UserId;  
                ";

            using var conn = await connectDb(_connectionString);

            return await conn.QueryFirstOrDefaultAsync<User>(query, new
            {
                UserID = userId
            });
        }

        public async Task DeleteUserByUserId(Guid userId)
        {
            const string query = @"
                    DELETE FROM user_base
                    WHERE id = @UserId;
                ";

            using var conn = await connectDb(_connectionString);

            await conn.ExecuteScalarAsync(query, new
            {
                UserID = userId.ToString()
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