using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Core.Domain.DbModels;
using Core.Domain.Models;
using Dapper;
using Infrastructure.Persistence.Repositories.Interfaces;
using Npgsql;

namespace Infrastructure.Persistence.Repositories
{
    public class UserSkillRepository : IUserSkillRepository
    {
        private readonly string _connectionString;

        public UserSkillRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public async Task<Guid> CreateUserSkill(Guid skillId, int level, string userId)
        {
            const string query = @"
                    Insert into user_skill (id, skill_id, skill_level, user_id)
                    VALUES (@Id, @SkillId, @Level, @UserId);
                ";

            var userSkillId = Guid.NewGuid();
            
            using var conn = await connectDb(_connectionString);
            await conn.ExecuteScalarAsync(query, new
            {
                Id = userSkillId,
                SkillId = skillId,
                Level = level,
                UserId = userId
            });

            return userSkillId;
        }
        
        public async Task<IEnumerable<UserSkillAndSkillModel>> GetAllUserSkills(string userId)
        {
            const string query = @"
                   SELECT user_skill.id AS IdString,
                            user_skill.skill_id AS SkillIdString,
                            user_skill.user_id AS UserIdString,
                            user_skill.skill_level AS SkillLevel,
                            skill.id AS IdString,
                            skill.skill_name AS SkillName
                    FROM user_skill
                    INNER JOIN skill ON user_skill.skill_id = skill.id
                    WHERE user_id = @UserId
                ";

            using var conn = await connectDb(_connectionString);
            var userSkillAndSkillDtos = await
                conn.QueryAsync<UserSkill, Skill, UserSkillAndSkillModel>
                (query, (userSkill, Skill) => new UserSkillAndSkillModel
                {
                    UserSkill = userSkill,
                    Skill = Skill
                }, new
                {
                    UserId = userId
                },
                    splitOn: "IdString");

            return userSkillAndSkillDtos;
        }

        public async Task DeleteUserSkill(string userId, string userSkillId)
        {
            const string query = @"
                   DELETE FROM user_skill
                   WHERE id = @Id
                   AND user_id = @UserId
                ";
            
            using var conn = await connectDb(_connectionString);

            await conn.ExecuteScalarAsync(query, new
            {
                Id = userSkillId,
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