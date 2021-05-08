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
                   SELECT id AS IdString,
                        user_id AS UserIdString,
                        driving_license_sv AS DrivingLicenseSv,
                        driving_license_en AS DrivingLicenseEn,
                        created_at AS CreatedAt,
                        updated_at AS UpdatedAt
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
                        driving_license_en = @DrivingLicenseEn
                    WHERE user_id = @UserId;

                   SELECT id AS IdString,
                        user_id AS UserIdString,
                        driving_license_sv AS DrivingLicenseSv,
                        driving_license_en AS DrivingLicenseEn,
                        created_at AS CreatedAt,
                        updated_at AS UpdatedAt
                   FROM other_information
                   WHERE user_id = @UserId;
                ";
            
            using var conn = await connectDb(_connectionString);
            return await conn.QueryFirstOrDefaultAsync<OtherInformation>(query, new
            {
                UserId = userId,
                otherInformation.DrivingLicenseSv,
                otherInformation.DrivingLicenseEn
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