using System;

namespace Core.Domain.Dtos
{
    public class OtherInformationDto
    {
        public Guid? Id { get; set; }
        
        public Guid? UserId { get; set; }
        
        public string DrivingLicenseSv { get; set; } = null!;

        public string DrivingLicenseEn { get; set; } = null!;

        public DateTime? CreatedAt { get; set; }
        
        public DateTime? UpdatedAt { get; set; }
    }
}