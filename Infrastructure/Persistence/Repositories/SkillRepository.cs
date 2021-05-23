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
    public class SkillRepository : ISkillRepository
    {
        private readonly string _connectionString;

        public SkillRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public async Task<Guid> CreateSkill(string skillName)
        {
            const string query = @"
                    Insert into skill (id, skill_name)
                    VALUES (@Id, @SkillName);  
                ";

            var skillId = Guid.NewGuid();
            
            using var conn = await connectDb(_connectionString);
            await conn.ExecuteScalarAsync(query, new
            {
                Id = skillId,
                SkillName = skillName
            });

            return skillId;
        }
        
        public async Task<IEnumerable<Skill>> GetAllSkills()
        {
            const string query = @"
                   SELECT id AS IdString,
                        skill_name AS SkillName
                   FROM skill
                ";

            using var conn = await connectDb(_connectionString);
            return await conn.QueryAsync<Skill>(query);
        }

        public async Task<Skill> GetSkillBySkillId(string skillId)
        {
            const string query = @"
                   SELECT id AS IdString,
                        skill_name AS SkillName
                   FROM skill
                   WHERE id = @Id;
                ";
            
            using var conn = await connectDb(_connectionString);
            return await conn.QueryFirstOrDefaultAsync<Skill>(query, new
            {
                Id = skillId
            });
        }

        public async Task DeleteSkillById(string skillId)
        {
            const string query = @"
                    DELETE FROM skill
                    WHERE id = @SkillId;
                    ";
            
            using var conn = await connectDb(_connectionString);
            await conn.ExecuteScalarAsync(query, new
            {
                SkillId = skillId
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