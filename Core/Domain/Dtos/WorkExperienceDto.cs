using System;

namespace Core.Domain.Dtos
{
    public class WorkExperienceDto
    {
        public Guid? Id { get; set; }
        
        public Guid? UserId { get; set; }
        
        public string EmploymentRate { get; set; }
        
        public string CompanyName { get; set; }
        
        public string RoleSv { get; set; }
        
        public string RoleEn { get; set; }
        
        public string DescriptionSv { get; set; }
        
        public string DescriptionEn { get; set; }
        
        public string CitySv { get; set; }
        
        public string CityEn { get; set; }
        
        public string CountrySv { get; set; }
        
        public string CountryEn { get; set; }
        
        public DateTime DateStarted { get; set; }
        
        public DateTime DateEnded { get; set; }
        
        public DateTime? CreatedAt { get; set; }
        
        public DateTime? UpdatedAt { get; set; }
    }
}