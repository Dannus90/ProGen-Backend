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
    public class LanguageRepository : ILanguageRepository
    {
        private readonly string _connectionString;

        public LanguageRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public async Task<Guid> CreateUserLanguage(string userId, Language language)
        {
            const string query = @"
                    Insert into language (id, user_id, language_sv, language_en)
                    VALUES (@Id, @UserId, @LanguageSv, @LanguageEn);  
                ";

            var languageId = Guid.NewGuid();
            
            using var conn = await connectDb(_connectionString);
            await conn.ExecuteScalarAsync(query, new
            {
                Id = languageId,
                UserId = userId,
                language.LanguageSv,
                language.LanguageEn
            });

            return languageId;
        }
        
        public async Task<Language> GetUserLanguage(string languageId)
        {
            const string query = @"
                   SELECT id AS IdString,
                        user_id AS UserIdString,
                        language_sv AS LanguageSv,
                        language_en AS LanguageEn
                   FROM language
                   WHERE id = @LanguageId;
                ";

            using var conn = await connectDb(_connectionString);
            return await conn.QueryFirstOrDefaultAsync<Language>(query, new
            {
                LanguageId = languageId
            });
        }
        
        public async Task<IEnumerable<Language>> GetUserLanguages(string userId)
        {
            const string query = @"
                   SELECT id AS IdString,
                        user_id AS UserIdString,
                        language_sv AS LanguageSv,
                        language_en AS LanguageEn
                   FROM language
                   WHERE user_id = @UserId;
                ";

            using var conn = await connectDb(_connectionString);
            return await conn.QueryAsync<Language>(query, new
            {
                UserId = userId
            });
        }
        
        public async Task<string> DeleteUserLanguage(string languageId)
        {
            const string query = @"
                   DELETE FROM language
                   WHERE id = @LanguageId 
                ";

            using var conn = await connectDb(_connectionString);
            await conn.ExecuteScalarAsync(query, new
            {
                LanguageId = languageId
            });

            return languageId;
        }
        
        private static async Task<IDbConnection> connectDb(string connectionString)
        {
            var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}