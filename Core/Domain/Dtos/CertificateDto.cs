using System;

namespace Core.Domain.Dtos
{
    public class CertificateDto
    {
        public Guid? Id { get; set; }
        public Guid? UserId { get; set; }
        public string? CertificateNameSv { get; set; }
        public string? CertificateNameEn { get; set; }
        public string? Organisation { get; set; }
        public string? IdentificationId { get; set; }
        public string? ReferenceAddress { get; set; }
        public DateTime? DateIssued { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}