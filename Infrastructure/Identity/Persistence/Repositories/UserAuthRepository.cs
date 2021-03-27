using System;
using System.Data;
using System.Threading.Tasks;

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
                    Insert into user_base(id, email, password)
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
    }
}