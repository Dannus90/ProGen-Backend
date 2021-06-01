using System;
using Core.Domain.DbModels;
using Core.Domain.Dtos;
using Core.Domain.Models;
using Core.Domain.ViewModels;

namespace Tests.UnitTests.ServiceTests.UserDataServiceMock
{
    public class UserDataServiceMockMethods
    {
        public static FullUserInformation GetFullUserInformation(string userId)
        {
            return new()
            {
                User = new User
                {
                    Email = "persson.daniel.1990@gmail.com",
                    Id = Guid.Parse(userId),
                    Password = "Testpassword",
                    FirstName = "Daniel",
                    LastName = "Persson"
                },
                UserData = new UserData
                {
                    CityEn = "Gothenburg",
                    CitySv = "Göteborg",
                    CountryEn = "Sweden",
                    CountrySv = "Sverige",
                    EmailCv = "Testmail@gmail.com",
                    PhoneNumber = "073-3249826",
                    AddressZipCode = "Testzip",
                    WorkTitleEn = "Work",
                    WorkTitleSv = "Arbete",
                    ProfileImage = "testsetset",
                    ProfileImagePublicId = "dsdfbsdfbsdfb"
                }
            };
        }

        public static UserDataDto GetUserDataDto(string userId)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse(userId),
                CityEn = "Gothenburg",
                CitySv = "Göteborg",
                CountryEn = "Sweden",
                CountrySv = "Sverige",
                EmailCv = "testmail@gmail.com",
                FirstName = "Daniel",
                LastName = "Persson",
                PhoneNumber = "073-3249826",
                AddressZipCode = "sdfgsdfg",
                ProfileImage = "sdfgsdfgsdfgsdfgsdfg",
                WorkTitleEn = "TestTitleEn",
                WorkTitleSv = "TestTitleSv",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }
        
        public static UserDataModel GetUserDataModel(string userId)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse(userId),
                CityEn = "Gothenburg",
                CitySv = "Göteborg",
                CountryEn = "Sweden",
                CountrySv = "Sverige",
                EmailCv = "testmail@gmail.com",
                PhoneNumber = "073-3249826",
                AddressZipCode = "sdfgsdfg",
                ProfileImage = "sdfgsdfgsdfgsdfgsdfg",
                WorkTitleEn = "TestTitleEn",
                WorkTitleSv = "TestTitleSv",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }
        
        public static UserData GetUserData (string userId)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse(userId),
                CityEn = "Gothenburg",
                CitySv = "Göteborg",
                CountryEn = "Sweden",
                CountrySv = "Sverige",
                EmailCv = "testmail@gmail.com",
                PhoneNumber = "073-3249826",
                AddressZipCode = "sdfgsdfg",
                ProfileImage = "sdfgsdfgsdfgsdfgsdfg",
                WorkTitleEn = "TestTitleEn",
                WorkTitleSv = "TestTitleSv",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }
    }
}