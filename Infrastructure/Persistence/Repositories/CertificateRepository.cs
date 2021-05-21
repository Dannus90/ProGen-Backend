using System;
using System.Data;
using System.Threading.Tasks;
using Core.Domain.DbModels;
using Dapper;
using Infrastructure.Persistence.Repositories.Interfaces;
using Npgsql;

namespace Infrastructure.Persistence.Repositories
{
    public class CertificateRepository : ICertificateRepository
    {
        private readonly string _connectionString;

        public CertificateRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Guid> CreateCertificate(Certificate certificate, string userId)
        {
            const string query = @"
                    Insert into certificate (id, user_id, organisation, 
                                                 certificate_name_sv, certificate_name_en,
                                                 reference_address, identification_id, date_issued)
                    VALUES (@Id, @UserId, @Organisation, @CertificateNameSv, @CertificateNameEn,
                            @ReferenceAddress, @IdentificationId, @DateIssued);  
                    ";

            var certificateId = Guid.NewGuid();

            using var conn = await connectDb(_connectionString);

            await conn.ExecuteScalarAsync(query, new
            {
                Id = certificateId,
                UserId = userId,
                certificate.Organisation,
                certificate.CertificateNameSv,
                certificate.CertificateNameEn,
                certificate.ReferenceAddress,
                certificate.IdentificationId,
                certificate.DateIssued
            });

            return certificateId;
        }
        
        private static async Task<IDbConnection> connectDb(string connectionString)
        {
            var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}