using System.Data;
using System.Threading.Tasks;
using Core.Domain.DbModels;
using Dapper;
using Infrastructure.Persistence.Repositories.Interfaces;
using Npgsql;

namespace Infrastructure.Persistence.Repositories
{
    public class OtherInformationRepository : IOtherInformationRepository
    {
        private readonly string _connectionString;

        public OtherInformationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public async Task<OtherInformation> GetOtherInformation(string userId)
        {
            const string query = @"
                   SELECT id AS Id,
                        user_id AS UserId,
                        driving_license_sv AS DrivingLicenseSv,
                        driving_license_en AS DrivingLicenseEn,
                        languages_sv AS LanguagesSv,
                        languages_en AS LanguagesEn
                   FROM other_information
                   WHERE user_id = @UserId;
                ";
            
            using var conn = await connectDb(_connectionString);
            return await conn.QueryFirstOrDefaultAsync<OtherInformation>(query, new
            {
                UserId = userId
            });
        }

        public async Task<OtherInformation> UpdateOtherInformation(string userId, OtherInformation otherInformation)
        {
            const string query = @"
                    UPDATE other_information
                    SET driving_license_sv = @DrivingLicenseSv,
                        driving_license_en = @DrivingLicenseEn,
                        languages_sv = @LanguagesSv,
                        languages_en = @LanguagesEn,
                    WHERE user_id = @UserId;

                   SELECT id AS Id,
                        user_id AS UserId,
                        driving_license_sv AS DrivingLicenseSv,
                        driving_license_en AS DrivingLicenseEn,
                        languages_sv AS LanguagesSv,
                        languages_en AS LanguagesEn
                   FROM other_information
                   WHERE user_id = @UserId;
                ";
            
            using var conn = await connectDb(_connectionString);
            return await conn.QueryFirstOrDefaultAsync<OtherInformation>(query, new
            {
                UserId = userId,
                otherInformation.DrivingLicenseSv,
                otherInformation.DrivingLicenseEn,
                otherInformation.LanguagesSv,
                otherInformation.LanguagesEn
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