using System;
using System.Data;
using System.Threading.Tasks;
using Core.Domain.DbModels;
using Dapper;
using Infrastructure.Persistence.Repositories.Interfaces;
using Npgsql;

namespace Infrastructure.Persistence.Repositories
{
    public class EducationRepository : IEducationRepository
    {
        private readonly string _connectionString;
        public EducationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public async Task<Guid> CreateEducation(Education education, string userId)
        {
            const string query = @"
                    Insert into education (id, user_id, education_name, 
                                                 exam_name, subject_area_sv, subject_area_en, 
                                                 description_sv, description_en,
                                                 city_sv, city_en,
                                                 country_sv, country_en,
                                                 date_started, date_ended)
                    VALUES (@Id, @UserId, @EducationName, @ExamName, @SubjectAreaSv, @SubjectAreaEn,
                    @DescriptionSv, @DescriptionEn, @Grade, @CitySv, @CityEn, @CountrySv, @CountryEn,
                    @DateStarted, @DateEnded);  
                ";

            var educationId = Guid.NewGuid();

            using var conn = await connectDb(_connectionString);

            await conn.ExecuteScalarAsync(query, new
            {
                Id = educationId,
                UserId = userId,
                education.EducationName,
                education.ExamName,
                education.SubjectAreaSv,
                education.SubjectAreaEn,
                education.DescriptionSv,
                education.DescriptionEn,
                education.Grade,
                education.CitySv,
                education.CityEn,
                education.CountrySv,
                education.CountryEn,
                education.DateStarted,
                education.DateEnded
            });

            return educationId;
        }
        
        private static async Task<IDbConnection> connectDb(string connectionString)
        {
            var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();
            
            return connection;
        }
    }
}