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

        private static async Task<IDbConnection> connectDb(string connectionString)
        {
            var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}