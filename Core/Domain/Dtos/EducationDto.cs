using System;

namespace Core.Domain.Dtos
{
    public class EducationDto
    {
        public Guid? Id { get; set; }
        
        public Guid? UserId { get; set; }
        
        public string EducationNameSv { get; set; } = null!;
        
        public string EducationNameEn { get; set; } = null!;
        
        public string ExamNameSv { get; set; } = null!;
        
        public string ExamNameEn { get; set; } = null!;
        
        public string SubjectAreaSv { get; set; } = null!;
        
        public string SubjectAreaEn { get; set; } = null!;
        
        public string DescriptionSv { get; set; } = null!;
        
        public string DescriptionEn { get; set; } = null!;
        
        public string Grade { get; set; } = null!;
        
        public string CitySv { get; set; } = null!;
        
        public string CityEn { get; set; } = null!;
        
        public string CountrySv { get; set; } = null!;
        
        public string CountryEn { get; set; } = null!;
        
        public DateTime? DateStarted { get; set; } = null!;
        
        public DateTime? DateEnded { get; set; }
        
        public DateTime? CreatedAt { get; set; }
        
        public DateTime? UpdatedAt { get; set; }
    }
}