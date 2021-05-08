using System;
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
        
        private static async Task<IDbConnection> connectDb(string connectionString)
        {
            var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}