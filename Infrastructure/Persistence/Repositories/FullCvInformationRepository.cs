using System.Data;
using System.Threading.Tasks;
using Core.Domain.DbModels;
using Core.Domain.ViewModels;
using Dapper;
using Infrastructure.Persistence.Repositories.Interfaces;
using Npgsql;

namespace Infrastructure.Persistence
{
    public class FullCvInformationRepository : IFullCvInformationRepository
    {
        private readonly string _connectionString;
        public FullCvInformationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public async Task<FullCvInformationViewModel> GetFullCvInformation(string userId)
        {
            const string query = @"
                   SELECT id AS IdString,
                            user_id AS UserIdString,
                            education_name_sv AS EducationNameSv,
                            education_name_en AS EducationNameEn,
                            exam_name_sv AS ExamNameSv,
                            exam_name_en AS ExamNameEn,
                            subject_area_sv AS SubjectAreaSv,
                            subject_area_en AS SubjectAreaEn,
                            description_sv AS DescriptionSv,
                            description_en AS DescriptionEn,
                            grade AS Grade,
                            city_sv AS CitySv,
                            city_en AS CityEn,
                            country_sv AS CountrySv,
                            country_en AS CountryEn,
                            date_started AS DateStarted,
                            date_ended AS DateEnded,
                            created_at AS CreatedAt,
                            updated_at AS UpdatedAt
                    FROM education WHERE id = @Id;  
                ";
            
            using var conn = await connectDb(_connectionString);

            await conn.QueryFirstOrDefaultAsync<Education>(query, new
            {
                Id = userId
            });

            return new FullCvInformationViewModel();
        }
        
        private static async Task<IDbConnection> connectDb(string connectionString)
        {
            var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();
            
            return connection;
        }
    }
}