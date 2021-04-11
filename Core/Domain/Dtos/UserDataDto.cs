using System;

namespace Core.Domain.Dtos
{
    public class UserDataDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailCv { get; set; }
        public string CitySv { get; set; }
        public string CityEn { get; set; }
        public string CountrySv { get; set; }
        public string CountryEn { get; set; }
    }
}