using System;
using System.Collections.Generic;
using Core.Domain.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Core.Context
{
    public static class Seed
    {
        public static void SeedDataBase(ModelBuilder modelBuilder)
        {
            // Initial user with password password123

            //////////////////////////////////
            ///         SEED USER          ///
            //////////////////////////////////

            var users = new List<User>()
            {
                new()
                {
                    Id = Guid.NewGuid(), Email = "testuser@gmail.com",
                    Password = "$2a$10$lmiYrmWUDf7klCsGo0VP.uI9DcK.5fUy2Ld34ahg8lQnIanlzThcy",
                    FirstName = "John", LastName = "Doe"
                }
            };

            modelBuilder.Entity<User>().HasData(users);
            
            //////////////////////////////////
            ///         SEED USERDATA      ///
            //////////////////////////////////
        
            var userData = new List<UserData>()
            {
                new()
                {
                    Id = Guid.NewGuid(), UserId = users[0].Id,
                    CityEn = "Gothenburg", CitySv = "Göteborg",
                    CountryEn = "Sweden", CountrySv = "Sverige",
                    PhoneNumber = "073-3249826", ProfileImage = "",
                    WorkTitleSv = "Mjukvaru utvecklare",
                    WorkTitleEn = "Software developer",
                    EmailCv = "persson.daniel.1990@gmail.com"
                }
            };

            modelBuilder.Entity<UserData>().HasData(userData);
            
            //////////////////////////////////
            ///     SEED PRESENTATIONS     ///
            //////////////////////////////////

            var userPresentations = new List<UserPresentation>()
            {
                new()
                {
                    Id = Guid.NewGuid(), UserId = users[0].Id,
                    PresentationEn = "PresentationText En",
                    PresentationSv = "PresentationText Sv"
                }
            };
            
            modelBuilder.Entity<UserPresentation>().HasData(userPresentations);
            
            //////////////////////////////////
            ///    SEED OTHERINFORMATION   ///
            //////////////////////////////////
            var otherInformations = new List<OtherInformation>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = users[0].Id,
                    DrivingLicenseEn = "Driving license B",
                    DrivingLicenseSv = "Körkort B"
                }
            };

            modelBuilder.Entity<OtherInformation>().HasData(otherInformations);
            
            //////////////////////////////////
            ///        SEED EDUCATION      ///
            //////////////////////////////////
            var educations = new List<Education>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = users[0].Id,
                    Grade = "VG",
                    CityEn = "Gothenburg",
                    CitySv = "Göteborg",
                    CountryEn = "Sweden",
                    CountrySv = "Sverige",
                    DescriptionEn = "Studies within social science and networking",
                    DescriptionSv = "Studier inom social vetenskap och nätverkande",
                    EducationNameEn = "Social Sciences",
                    EducationNameSv = "Social vetenskap",
                    ExamNameEn = "Bacheclor with social science",
                    ExamNameSv = "Kandidatexamen inom socialvetenskap",
                    SubjectAreaEn = "Behavioral science",
                    SubjectAreaSv = "Beteende vetenskap"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = users[0].Id,
                    Grade = "VG",
                    CityEn = "Gothenburg2",
                    CitySv = "Göteborg2",
                    CountryEn = "Sweden2",
                    CountrySv = "Sverige2",
                    DescriptionEn = "Studies within social science and networking2",
                    DescriptionSv = "Studier inom social vetenskap och nätverkande2",
                    EducationNameEn = "Social Sciences2",
                    EducationNameSv = "Social vetenskap2",
                    ExamNameEn = "Bacheclor with social science2",
                    ExamNameSv = "Kandidatexamen inom socialvetenskap2",
                    SubjectAreaEn = "Behavioral science2",
                    SubjectAreaSv = "Beteende vetenskap2"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = users[0].Id,
                    Grade = "VG",
                    CityEn = "Gothenburg3",
                    CitySv = "Göteborg3",
                    CountryEn = "Sweden3",
                    CountrySv = "Sverige3",
                    DescriptionEn = "Studies within social science and networking3",
                    DescriptionSv = "Studier inom social vetenskap och nätverkande3",
                    EducationNameEn = "Social Sciences3",
                    EducationNameSv = "Social vetenskap3",
                    ExamNameEn = "Bacheclor with social science3",
                    ExamNameSv = "Kandidatexamen inom socialvetenskap3",
                    SubjectAreaEn = "Behavioral science3",
                    SubjectAreaSv = "Beteende vetenskap3"
                }
            };
                
            modelBuilder.Entity<Education>().HasData(educations);
            
            //////////////////////////////////
            ///    SEED WORKEXPERIENCE     ///
            //////////////////////////////////
            var workExperiences = new List<WorkExperience>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = users[0].Id,
                    CityEn = "GothenBurg",
                    CitySv = "Göteborg",
                    CompanyName = "FrontEdge IT",
                    CountryEn = "Sweden",
                    CountrySv = "Sverige",
                    DescriptionEn = "Worked as a fullstack developer on a consultant company",
                    DescriptionSv = "Arbetade som en fullstackutvecklare på ett IT företag",
                    EmploymentRate = "Fulltime",
                    RoleEn = "Software Developer",
                    RoleSv = "Mjukvaru-utvecklare"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = users[0].Id,
                    CityEn = "GothenBurg2",
                    CitySv = "Göteborg2",
                    CompanyName = "FrontEdge IT2",
                    CountryEn = "Sweden2",
                    CountrySv = "Sverige2",
                    DescriptionEn = "Worked as a fullstack developer on a consultant company2",
                    DescriptionSv = "Arbetade som en fullstackutvecklare på ett IT företag2",
                    EmploymentRate = "Fulltime2",
                    RoleEn = "Software Developer2",
                    RoleSv = "Mjukvaru-utvecklare2"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = users[0].Id,
                    CityEn = "GothenBurg3",
                    CitySv = "Göteborg3",
                    CompanyName = "FrontEdge IT3",
                    CountryEn = "Sweden3",
                    CountrySv = "Sverige3",
                    DescriptionEn = "Worked as a fullstack developer on a consultant company3",
                    DescriptionSv = "Arbetade som en fullstackutvecklare på ett IT företag3",
                    EmploymentRate = "Fulltime3",
                    RoleEn = "Software Developer3",
                    RoleSv = "Mjukvaru-utvecklare3"
                }
            };
            
            modelBuilder.Entity<WorkExperience>().HasData(workExperiences);
            
            //////////////////////////////////
            ///       SEED LANGUAGES       ///
            //////////////////////////////////
            var languages = new List<Language>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = users[0].Id,
                    LanguageSv = "Svenska",
                    LanguageEn = "Swedish"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = users[0].Id,
                    LanguageSv = "Engelska",
                    LanguageEn = "English"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = users[0].Id,
                    LanguageSv = "Spanska",
                    LanguageEn = "Spanish"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = users[0].Id,
                    LanguageSv = "Franska",
                    LanguageEn = "French"
                }
            };
            
            modelBuilder.Entity<Language>().HasData(languages);
        }
    }
}