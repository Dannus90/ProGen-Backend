using System;
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

        public async Task RegisterUser(string hashedPassword, string email)
        {
            const string query =
                @"
                    INSERT INTO `user`
                    (`email`, `password`)
                    VALUES (@Email, @Password);
                ";

            await using (var dbCon = new NpgsqlConnection(_connectionString))
            {
                dbCon.ExecuteScalarAsync(query, new
                {
                    Email = email,
                    Password = hashedPassword
                });
                
                await dbCon.CloseAsync();
            }
        }
    }
}