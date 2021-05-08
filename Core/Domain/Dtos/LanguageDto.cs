using System;

namespace Core.Domain.Dtos
{
    public class LanguageDto
    {
        public Guid? Id { get; set; }
        public Guid? UserId { get; set; }
        public string LanguageSv { get; set; }
        public string LanguageEn { get; set; }
    }
}