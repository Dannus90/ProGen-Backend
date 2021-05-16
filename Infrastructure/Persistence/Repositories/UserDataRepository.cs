using System;
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
                            user_base.first_name AS Firstname,
                            user_base.last_name AS Lastname,
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
                            user_data.zip_code AS ZipCode,
                            user_data.profile_image AS ProfileImage,
                            user_data.profile_image_public_id AS ProfileImagePublicId,
                            user_data.work_title_sv AS WorkTitleSv,
                            user_data.work_title_en AS WorkTitleEn,
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

        public async Task<UserData> UpdateUserData(string userId, UserDataModel userData)
        {
            Console.WriteLine(userData.ZipCode);
            Console.WriteLine(userData.ZipCode);
            Console.WriteLine(userData.ZipCode);
            Console.WriteLine(userData.ZipCode);
            Console.WriteLine(userData.ZipCode);
            const string query = @"
                    UPDATE user_data
                    SET email_cv = @EmailCv,
                        city_sv = @CitySv,
                        city_en = @CityEn,
                        country_sv = @CountrySv,
                        country_en = @CountryEn,
                        zip_code = @ZipCode,
                        phone_number = @PhoneNumber,
                        work_title_sv = @WorkTitleSv,
                        work_title_en = @WorkTitleEn
                    WHERE user_id = @UserId;

                   SELECT id AS IdString,
                        user_id AS UserIdString,
                        phone_number AS PhoneNumber,
                        email_cv AS EmailCv,
                        city_sv AS CitySv,
                        city_en AS CityEn,
                        country_sv AS CountrySv,
                        country_en AS CountryEn,
                        zip_code AS ZipCode,
                        profile_image AS ProfileImage,
                        profile_image_public_id AS ProfileImagePublicId,
                        work_title_sv AS WorkTitleSv,
                        work_title_en AS WorkTitleEn,
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
                userData.ZipCode,
                userData.PhoneNumber,
                userData.WorkTitleSv,
                userData.WorkTitleEn
            });
        }
        
        public async Task<ProfileImageModel> UploadProfileImage(string imagePublicId, string imageUrl, string userId)
        {
            const string query = @"
                    UPDATE user_data
                    SET profile_image_public_id = @ProfileImagePublicId,
                        profile_image = @ProfileImage
                    WHERE user_id = @UserId;

                   SELECT profile_image_public_id AS ProfileImagePublicId,
                        profile_image AS ProfileImage
                   FROM user_data
                   WHERE user_id = @UserId;
                ";
            
            using var conn = await connectDb(_connectionString);
            return await conn.QueryFirstOrDefaultAsync<ProfileImageModel>(query, new
            {
                UserId = userId,
                ProfileImagePublicId = imagePublicId,
                ProfileImage = imageUrl
            });
        }
        
        public async Task DeleteProfileImage(string userId)
        {
            const string query = @"
                    UPDATE user_data
                    SET profile_image_public_id = null,
                        profile_image = null
                    WHERE user_id = @UserId;
                ";
            
            using var conn = await connectDb(_connectionString);
            await conn.ExecuteScalarAsync(query, new
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