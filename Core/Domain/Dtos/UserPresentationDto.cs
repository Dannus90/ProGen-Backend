using System;

namespace Core.Domain.Dtos
{
    public class UserPresentationDto
    {
        public Guid? Id { get; set;}
        public Guid? UserId { get; set; }
        public string PresentationSv { get; set; }
        public string PresentationEn { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}