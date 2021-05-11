using System;

namespace Core.Domain.Dtos
{
    public class WorkExperienceDto
    {
        public Guid? Id { get; set; }
        
        public Guid? UserId { get; set; }

        public string EmploymentRate { get; set; } = null!;
        
        public string CompanyName { get; set; } = null!;
        
        public string RoleSv { get; set; } = null!;
        
        public string RoleEn { get; set; } = null!;
        
        public string DescriptionSv { get; set; } = null!;
        
        public string DescriptionEn { get; set; } = null!;
        
        public string CitySv { get; set; } = null!;
        
        public string CityEn { get; set; } = null!;
        
        public string CountrySv { get; set; } = null!;
        
        public string CountryEn { get; set; } = null!;
        
        public DateTime? DateStarted { get; set; }
        
        public DateTime? DateEnded { get; set; }
        
        public DateTime? CreatedAt { get; set; }
        
        public DateTime? UpdatedAt { get; set; }
    }
}