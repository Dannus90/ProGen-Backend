using System.Data;
using System.Threading.Tasks;
using Core.Domain.DbModels;
using Dapper;
using Infrastructure.Persistence.Repositories.Interfaces;
using Npgsql;

namespace Infrastructure.Persistence.Repositories
{
    public class UserAuthRepository : IUserRepository
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

        public async Task<User> GetUserByEmail(string email)
        {
            var query = 
                @"
                    SELECT `id` AS Id,
                            `email` AS Email,
                            `password` AS Password,
                            `last_login` AS LastLogin,
                            `created_at` AS CreatedAt,
                            `updated_at` AS UpdatedAt   
                    FROM user_base WHERE email = @Email;  
                ";
            
            using var conn = connectDb(_connectionString);

            return await conn.QueryFirstOrDefaultAsync<User>(query, new
            {
                Email = email
            });
        }
    }
}