using System;
using System.Threading.Tasks;
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

        public async Task RegisterUser(string password, string email)
        {
            const string query =
                @"

                ";

            await using (var dbCon = new NpgsqlConnection(_connectionString))
            {
                
            }

            Console.WriteLine("HELLO!");
        }
    }
}