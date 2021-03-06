using System;
using System.Collections.Generic;
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
                    Insert into education (id, user_id, education_name_sv, 
                                                 education_name_en, exam_name_sv,
                                                 exam_name_en, subject_area_sv, subject_area_en, 
                                                 description_sv, description_en,
                                                 grade,
                                                 city_sv, city_en,
                                                 country_sv, country_en,
                                                 date_started, date_ended)
                    VALUES (@Id, @UserId, @EducationNameSv, @EducationNameEn, @ExamNameSv, @ExamNameEn, @SubjectAreaSv, @SubjectAreaEn,
                    @DescriptionSv, @DescriptionEn, @Grade, @CitySv, @CityEn, @CountrySv, @CountryEn,
                    @DateStarted, @DateEnded);  
                ";

            var educationId = Guid.NewGuid();

            using var conn = await connectDb(_connectionString);

            await conn.ExecuteScalarAsync(query, new
            {
                Id = educationId,
                UserId = userId,
                education.EducationNameSv,
                education.EducationNameEn,
                education.ExamNameSv,
                education.ExamNameEn,
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
        
        public async Task<Education> UpdateEducation
            (string educationId, Education education, string userId)
        {
            const string query = @"
                    UPDATE education
                    SET city_sv = @CitySv,
                        city_en = @CityEn,
                        grade = @Grade,
                        country_sv = @CountrySv,
                        country_en = @CountryEn,
                        description_sv = @DescriptionSv,
                        description_en = @DescriptionEn,
                        date_started = @DateStarted,
                        date_ended = @DateEnded,
                        education_name_sv = @EducationNameSv,
                        education_name_En = @EducationNameEn,
                        exam_name_sv = @ExamNameSv,
                        exam_name_en = @ExamNameEn,
                        subject_area_sv = @SubjectAreaSv,
                        subject_area_en = @SubjectAreaEn
                    WHERE id = @Id
                    AND user_id = @UserId;

                   SELECT id AS IdString,
                        user_id AS UserIdString,
                        city_sv AS CitySv,
                        city_en AS CityEn,
                        grade AS Grade,
                        country_sv AS CountrySv,
                        country_en AS CountryEn,
                        description_sv AS DescriptionSv,
                        description_en AS DescriptionEn,
                        date_started AS DateStarted,
                        date_ended AS DateEnded,
                        education_name_sv AS EducationNameSv,
                        education_name_en AS EducationNameEn,
                        exam_name_sv AS ExamNameSv,
                        exam_name_en AS ExamNameEn,
                        subject_area_sv AS SubjectAreaSv,
                        subject_area_en AS SubjectAreaEn,
                        created_at AS CreatedAt,
                        updated_at AS UpdatedAt
                   FROM education
                   WHERE id = @Id
                   AND user_id = @UserId;
                ";
            
            using var conn = await connectDb(_connectionString);

            return await conn.QueryFirstOrDefaultAsync<Education>(query, new
            {
                Id = educationId,
                education.CitySv,
                education.CityEn,
                education.Grade,
                education.CountrySv,
                education.CountryEn,
                education.DescriptionSv,
                education.DescriptionEn,
                education.DateStarted,
                education.DateEnded,
                education.EducationNameSv,
                education.EducationNameEn,
                education.ExamNameSv,
                education.ExamNameEn,
                education.SubjectAreaSv,
                education.SubjectAreaEn,
                UserId = userId
            });
        }
        
        public async Task<Education> GetEducation(string educationId, string userId)
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
                    FROM education WHERE id = @Id
                    AND user_id = @UserId;  
                ";
            
            using var conn = await connectDb(_connectionString);

            return await conn.QueryFirstOrDefaultAsync<Education>(query, new
            {
                Id = educationId,
                UserId = userId
            });
        }
        
        public async Task<IEnumerable<Education>> GetEducations(string userId)
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
                    FROM education WHERE user_id = @UserId;
                ";

            using var conn = await connectDb(_connectionString);

            return await conn.QueryAsync<Education>(query, new
            {
                UserId = userId
            });
        }
        
        public async Task DeleteEducation(string educationId, string userId)
        {
            const string query = @"
                   DELETE FROM education
                   WHERE id = @Id
                   AND user_id = @UserId
                ";
            
            using var conn = await connectDb(_connectionString);

            await conn.ExecuteScalarAsync(query, new
            {
                Id = educationId,
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