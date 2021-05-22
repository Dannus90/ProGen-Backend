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
    public class WorkExperienceRepository : IWorkExperienceRepository
    {
        private readonly string _connectionString;

        public WorkExperienceRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public async Task<Guid> CreateWorkExperience(WorkExperience workExperience, string userId)
        {
            const string query = @"
                    Insert into work_experience (id, user_id, employment_rate, 
                                                 company_name, role_sv, role_en, 
                                                 description_sv, description_en,
                                                 city_sv, city_en,
                                                 country_sv, country_en,
                                                 date_started, date_ended)
                    VALUES (@Id, @UserId, @EmploymentRate, @CompanyName, @RoleSv, @RoleEn,
                    @DescriptionSv, @DescriptionEn, @CitySv, @CityEn, @CountrySv, @CountryEn,
                    @DateStarted, @DateEnded);  
                ";

            var workExperienceId = Guid.NewGuid();

            using var conn = await connectDb(_connectionString);

            await conn.ExecuteScalarAsync(query, new
            {
                Id = workExperienceId,
                UserId = userId,
                workExperience.EmploymentRate,
                workExperience.CompanyName,
                workExperience.RoleSv,
                workExperience.RoleEn,
                workExperience.DescriptionSv,
                workExperience.DescriptionEn,
                workExperience.CitySv,
                workExperience.CityEn,
                workExperience.CountrySv,
                workExperience.CountryEn,
                workExperience.DateStarted,
                workExperience.DateEnded
            });

            return workExperienceId;
        }
        
        public async Task<IEnumerable<WorkExperience>> GetWorkExperiences(string userId)
        {
            const string query = @"
                   SELECT id AS IdString,
                            user_id AS UserIdString,
                            employment_rate AS EmploymentRate,
                            company_name AS CompanyName,
                            role_sv AS RoleSv,
                            role_en AS RoleEn,
                            description_sv AS DescriptionSv,
                            description_en AS DescriptionEn,
                            city_sv AS CitySv,
                            city_en AS CityEn,
                            country_sv AS CountrySv,
                            country_en AS CountryEn,
                            date_started AS DateStarted,
                            date_ended AS DateEnded,
                            created_at AS CreatedAt,
                            updated_at AS UpdatedAt
                    FROM work_experience WHERE user_id = @UserId;
                ";

            using var conn = await connectDb(_connectionString);

            return await conn.QueryAsync<WorkExperience>(query, new
            {
                UserId = userId
            });
        }
        
        public async Task<WorkExperience> GetWorkExperience(string workExperienceId, string userId)
        {
            const string query = @"
                   SELECT id AS IdString,
                            user_id AS UserIdString,
                            employment_rate AS EmploymentRate,
                            company_name AS CompanyName,
                            role_sv AS RoleSv,
                            role_en AS RoleEn,
                            description_sv AS DescriptionSv,
                            description_en AS DescriptionEn,
                            city_sv AS CitySv,
                            city_en AS CityEn,
                            country_sv AS CountrySv,
                            country_en AS CountryEn,
                            date_started AS DateStarted,
                            date_ended AS DateEnded,
                            created_at AS CreatedAt,
                            updated_at AS UpdatedAt
                    FROM work_experience WHERE id = @Id
                    AND user_id = @UserId;
                ";
            
            using var conn = await connectDb(_connectionString);

            return await conn.QueryFirstOrDefaultAsync<WorkExperience>(query, new
            {
                Id = workExperienceId,
                UserId = userId
            });
        }
        
        public async Task DeleteWorkExperience(string workExperienceId, string userId)
        {
            const string query = @"
                   DELETE FROM work_experience
                   WHERE id = @Id
                   AND user_id = @UserId
                ";
            
            using var conn = await connectDb(_connectionString);

            await conn.ExecuteScalarAsync(query, new
            {
                Id = workExperienceId,
                UserId = userId
            });
        }
        
        public async Task<WorkExperience> UpdateWorkExperience
            (string workExperienceId, WorkExperience workExperience, string userId)
        {
            const string query = @"
                    UPDATE work_experience
                    SET city_sv = @CitySv,
                        city_en = @CityEn,
                        company_name = @CompanyName,
                        country_sv = @CountrySv,
                        country_en = @CountryEn,
                        description_sv = @DescriptionSv,
                        description_en = @DescriptionEn,
                        date_started = @DateStarted,
                        date_ended = @DateEnded,
                        employment_rate = @EmploymentRate,
                        role_sv = @RoleSv,
                        role_en = @RoleEn
                    WHERE id = @Id
                    AND user_id = @UserId;

                   SELECT id AS IdString,
                        user_id AS UserIdString,
                        city_sv AS CitySv,
                        city_en AS CityEn,
                        company_name AS CompanyName,
                        country_sv AS CountrySv,
                        country_en AS CountryEn,
                        description_sv AS DescriptionSv,
                        description_en AS DescriptionEn,
                        date_started AS DateStarted,
                        date_ended AS DateEnded,
                        employment_rate AS EmploymentRate,
                        role_sv AS RoleSv,
                        role_en AS RoleEn
                   FROM work_experience
                   WHERE id = @Id
                   AND user_id = @UserId;
                ";
            
            using var conn = await connectDb(_connectionString);

            return await conn.QueryFirstOrDefaultAsync<WorkExperience>(query, new
            {
                Id = workExperienceId,
                workExperience.CitySv,
                workExperience.CityEn,
                workExperience.CompanyName,
                workExperience.CountrySv,
                workExperience.CountryEn,
                workExperience.DescriptionSv,
                workExperience.DescriptionEn,
                workExperience.DateStarted,
                workExperience.DateEnded,
                workExperience.EmploymentRate,
                workExperience.RoleEn,
                workExperience.RoleSv,
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