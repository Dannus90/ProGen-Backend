using System;

namespace Core.Domain.Dtos
{
    public class EducationDto
    {
        public Guid? Id { get; set; }
        
        public Guid? UserId { get; set; }
        
        public string EducationNameSv { get; set; }
        
        public string EducationNameEn { get; set; }
        
        public string ExamNameSv { get; set; }
        
        public string ExamNameEn { get; set; }
        
        public string SubjectAreaSv { get; set; }
        
        public string SubjectAreaEn { get; set; }
        
        public string DescriptionSv { get; set; }
        
        public string DescriptionEn { get; set; }
        
        public string Grade { get; set; }
        
        public string CitySv { get; set; }
        
        public string CityEn { get; set; }
        
        public string CountrySv { get; set; }
        
        public string CountryEn { get; set; }
        
        public DateTime? DateStarted { get; set; }
        
        public DateTime? DateEnded { get; set; }
        
        public DateTime? CreatedAt { get; set; }
        
        public DateTime? UpdatedAt { get; set; }
    }
}