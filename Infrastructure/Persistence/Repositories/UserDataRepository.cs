
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain.DbModels;
using Core.Domain.Models;
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
            const string query = @"
                    SELECT user_base.id AS IdString,
                            user_base.email AS Email,
                            user_base.firstname AS Firstname,
                            user_base.lastname AS Lastname,
                            user_base.last_login AS LastLogin,
                            user_base.created_at AS CreatedAt,
                            user_base.updated_at AS UpdatedAt,
                            user_data.id AS IdString,
                            user_data.user_id AS UserIdString,
                            user_data.phone_number AS PhoneNumber,
                            user_data.email_cv AS EmailCv,
                            user_data.city_sv AS CitySv,
                            user_data.city_en AS CityEn,
                            user_data.country_sv AS CountrySv,
                            user_data.country_en AS CountryEn,
                            user_data.profile_image AS ProfileImage,
                            user_data.created_at AS CreatedAt,
                            user_data.updated_at AS UpdatedAt
                    FROM user_base
                        LEFT JOIN user_data ON user_base.id = user_data.user_id
                    WHERE user_base.id = @UserId;
                ";

            using var conn = await connectDb(_connectionString);

            var result = await conn.QueryAsync<User, UserData, FullUserInformation>
            (query, (user, userData) => new FullUserInformation()
                {
                    User = user,
                    UserData = userData
                },
                new
                {
                    UserId = userId
                }, splitOn: "IdString");
            return result.ToList()[0];
        }

        public async Task<UserData> UpdateUserData(string userId, UserData userData)
        {
            const string query = @"
                    UPDATE user_data
                    SET email_cv = @EmailCv,
                        city_sv = @CitySv,
                        city_en = @CityEn,
                        country_sv = @CountrySv,
                        country_en = @CountryEn,
                        phone_number = @PhoneNumber
                    WHERE user_id = @UserId;

                   SELECT id AS IdString,
                        user_id AS UserIdString,
                        phone_number AS PhoneNumber,
                        email_cv AS EmailCv,
                        city_sv AS CitySv,
                        city_en AS CityEn,
                        country_sv AS CountrySv,
                        country_en AS CountryEn,
                        profile_image AS ProfileImage,
                        created_at AS CreatedAt,
                        updated_at AS UpdatedAt
                   FROM user_data
                   WHERE user_id = @UserId;
                ";
            
            using var conn = await connectDb(_connectionString);
            return await conn.QueryFirstOrDefaultAsync<UserData>(query, new
            {
                UserId = userId,
                userData.EmailCv,
                userData.CitySv,
                userData.CityEn,
                userData.CountrySv,
                userData.CountryEn,
                userData.PhoneNumber
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