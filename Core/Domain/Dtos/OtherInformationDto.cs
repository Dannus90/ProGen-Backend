using System;

namespace Core.Domain.Dtos
{
    public class OtherInformationDto
    {
        public Guid? Id { get; set; }
        
        public Guid? UserId { get; set; }
        
        public string DrivingLicenseSv { get; set; }

        public string DrivingLicenseEn { get; set; }

        public DateTime? CreatedAt { get; set; }
        
        public DateTime? UpdatedAt { get; set; }
    }
}