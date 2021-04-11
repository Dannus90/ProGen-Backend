using System;
using System.Data;
using System.Threading.Tasks;
using Core.Domain.DbModels;
using Core.Domain.Dtos;
using Dapper;
using Infrastructure.Persistence.Repositories.Interfaces;
using Npgsql;

namespace Infrastructure.Persistence.Repositories
{
    public class UserDataRepository : IUserDataRepository
    {
        private readonly string _connectionString;

        public UserDataRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<FullUserInformation> GetFullUserInformation(string userId)
        {
            var fullUserInformation = new FullUserInformation();
            const string query = @"
                    SELECT `user_base`.`id` AS IdString,
                            `user_base`.`email` AS Email,
                            `user_base`.`firstname` AS Firstname,
                            `user_base`.`lastname` AS Lastname,
                            `user_base`.`last_login` AS LastLogin,
                            `user_base`.`created_at` AS CreatedAt,
                            `user_base`.`updated_at` AS UpdatedAt,
                            `user_data`.`id` AS IdString,
                            `user_data`.`user_id` AS UserId,
                            `user_data`.`phone_number` AS PhoneNumber,
                            `user_data`.`email_cv` AS EmailCv,
                            `user_data`.`city_sv` AS CitySv,
                            `user_data`.`city_en` AS CityEn,
                            `user_data`.`country_sv` AS CountrySv,
                            `user_data`.`country_en` AS CountryEn,
                            `user_data`.`profile_image` AS ProfileImage,
                            `user_data`.`created_at` AS CreatedAt,
                            `user_data`.`updated_at` AS UpdatedAt
                    FROM `user_base`
                        LEFT JOIN `user_data` ON `user_base`.`id` = `user_data`.`user_id`
                    WHERE `user_base`.`user_id` = @UserId;
                ";

            using var conn = await connectDb(_connectionString);

            await conn.QueryAsync<User, UserData, User>(query, (user, userData) =>
                {
                    fullUserInformation.User ??= user;
                    fullUserInformation.UserData ??= userData;
                    return null;
                },
                new
                {
                    UserId = userId
                });
            return fullUserInformation;
        }
        
        private static async Task<IDbConnection> connectDb(string connectionString)
        {
            var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}