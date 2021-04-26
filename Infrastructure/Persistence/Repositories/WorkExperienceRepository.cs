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
        
        public async Task CreateWorkExperience(WorkExperience workExperience, string userId)
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
                EmploymentRate = workExperience.EmploymentRate,
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

            var workExperienceId = Guid.NewGuid();

            using var conn = await connectDb(_connectionString);

            return await conn.QueryAsync<WorkExperience>(query, new
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