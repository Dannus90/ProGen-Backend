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

        public async Task<IEnumerable<Certificate>> GetAllCertificatesForUser(string userId)
        {
            const string query = @"
                   SELECT id AS IdString,
                            user_id AS UserIdString,
                            certificate_name_sv AS CertificateNameSv,
                            certificate_name_en AS CertificateNameEn,
                            organisation AS Organisation,
                            identification_id AS IdentificationId,
                            reference_address AS ReferenceAddress,
                            date_issued AS DateIssued,
                            created_at AS CreatedAt,
                            updated_at AS UpdatedAt
                    FROM certificate WHERE user_id = @UserId;
                ";

            using var conn = await connectDb(_connectionString);

            return await conn.QueryAsync<Certificate>(query, new
            {
                UserId = userId
            });
        }
        
        public async Task<Certificate> GetCertificateForUser(string certificateId, string userId)
        {
            const string query = @"
                   SELECT id AS IdString,
                            user_id AS UserIdString,
                            certificate_name_sv AS CertificateNameSv,
                            certificate_name_en AS CertificateNameEn,
                            organisation AS Organisation,
                            identification_id AS IdentificationId,
                            reference_address AS ReferenceAddress,
                            date_issued AS DateIssued,
                            created_at AS CreatedAt,
                            updated_at AS UpdatedAt
                    FROM certificate WHERE id = @CertificateId
                    AND user_id = @UserId;
                ";

            using var conn = await connectDb(_connectionString);

            return await conn.QueryFirstOrDefaultAsync<Certificate>(query, new
            {
                CertificateId = certificateId,
                UserId = userId
            });
        }
        
        public async Task<Certificate> UpdateCertificateForUser
            (string certificateId, Certificate certificate, string userId)
        {
            const string query = @"
                    UPDATE certificate
                    SET organisation = @Organisation,
                        date_issued = @DateIssued,
                        identification_id = @IdentificationId,
                        reference_address = @ReferenceAddress,
                        certificate_name_sv = @CertificateNameSv,
                        certificate_name_en = @CertificateNameEn
                    WHERE id = @Id
                    AND user_id = @UserId;

                   SELECT id AS IdString,
                        user_id AS UserIdString,
                        organisation AS Organisation,
                        date_issued AS DateIssued,
                        identification_id AS IdentificationId,
                        reference_address AS ReferenceAddress,
                        certificate_name_sv AS CertificateNameSv,
                        certificate_name_en AS CertificateNameEn,
                        created_at AS CreatedAt,
                        updated_at AS UpdatedAt
                   FROM certificate
                   WHERE id = @Id
                   AND user_id = @UserId;
                ";
            
            using var conn = await connectDb(_connectionString);

            return await conn.QueryFirstOrDefaultAsync<Certificate>(query, new
            {
                Id = certificateId,
                certificate.Organisation,
                certificate.DateIssued,
                certificate.IdentificationId,
                certificate.ReferenceAddress,
                certificate.CertificateNameSv,
                certificate.CertificateNameEn,
                UserId = userId
            });
        }
        
        public async Task DeleteSingleCertificateForUser(string certificateId, string userId)
        {
            const string query = @"
                   DELETE FROM certificate
                   WHERE id = @Id
                   AND user_id = @UserId
                ";
            
            using var conn = await connectDb(_connectionString);

            await conn.ExecuteScalarAsync(query, new
            {
                Id = certificateId,
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