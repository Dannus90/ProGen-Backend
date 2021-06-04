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
                    FirstName = "Daniel", LastName = "Persson"
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
                    AddressZipCode = "Ponnygatan 3, 43131",
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
                    PresentationEn = "Lorem Ipsum is simply dummy text of the printing and typesetting industry." +
                                     " Lorem Ipsum has been the industry's standard dummy text ever since the 1500s," +
                                     " when an unknown printer took a galley of type and scrambled it to make a type specimen book." +
                                     " It has survived not only five centuries, but also the leap into electronic typesetting," +
                                     " remaining essentially unchanged." +
                                     " It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages," +
                                     " and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                    PresentationSv = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. " +
                                     "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, " +
                                     "when an unknown printer took a galley of type and scrambled it to make a type specimen book. " +
                                     "It has survived not only five centuries, but also the leap into electronic typesetting, " +
                                     "remaining essentially unchanged. " +
                                     "It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, " +
                                     "and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum."
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
                    DescriptionEn = "There are many variations of passages of Lorem Ipsum available, " +
                                    "but the majority have suffered alteration in some form," +
                                    " by injected humour, or randomised words which don't look even slightly believable. ",
                    DescriptionSv = "There are many variations of passages of Lorem Ipsum available, " +
                                    "but the majority have suffered alteration in some form," +
                                    " by injected humour, or randomised words which don't look even slightly believable. ",
                    EducationNameEn = "Social Sciences",
                    EducationNameSv = "Social vetenskap",
                    ExamNameEn = "Bacheclor with social science",
                    ExamNameSv = "Kandidatexamen inom socialvetenskap",
                    SubjectAreaEn = "Behavioral science",
                    SubjectAreaSv = "Beteendevetenskap",
                    DateStarted = DateTime.Now.AddDays(-400),
                    DateEnded = DateTime.Now.AddDays(-250)
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = users[0].Id,
                    Grade = "VG",
                    CityEn = "Gothenburg",
                    CitySv = "Göteborg",
                    CountryEn = "Sweden",
                    CountrySv = "Sverige",
                    DescriptionEn = "There are many variations of passages of Lorem Ipsum available, " +
                                    "but the majority have suffered alteration in some form, by injected humour," +
                                    " or randomised words which don't look even slightly believable. ",
                    DescriptionSv = "There are many variations of passages of Lorem Ipsum available, " +
                                    "but the majority have suffered alteration in some form, by injected humour, " +
                                    "or randomised words which don't look even slightly believable. ",
                    EducationNameEn = "Social Sciences",
                    EducationNameSv = "Social vetenskap",
                    ExamNameEn = "Bacheclor with social science",
                    ExamNameSv = "Kandidatexamen inom socialvetenskap",
                    SubjectAreaEn = "Behavioral science",
                    SubjectAreaSv = "Beteendevetenskap",
                    DateStarted = DateTime.Now.AddDays(-600),
                    DateEnded = DateTime.Now.AddDays(-401)
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = users[0].Id,
                    Grade = "VG",
                    CityEn = "Gothenburg",
                    CitySv = "Göteborg",
                    CountryEn = "Sweden",
                    CountrySv = "Sverige",
                    DescriptionEn = "There are many variations of passages of Lorem Ipsum available," +
                                    " but the majority have suffered alteration in some form, by injected humour," +
                                    " or randomised words which don't look even slightly believable. ",
                    DescriptionSv = "There are many variations of passages of Lorem Ipsum available," +
                                    " but the majority have suffered alteration in some form, by injected humour," +
                                    " or randomised words which don't look even slightly believable. ",
                    EducationNameEn = "Social Sciences",
                    EducationNameSv = "Social vetenskap",
                    ExamNameEn = "Bacheclor with social science",
                    ExamNameSv = "Kandidatexamen inom socialvetenskap",
                    SubjectAreaEn = "Behavioral science",
                    SubjectAreaSv = "Beteende vetenskap",
                    DateStarted = DateTime.Now.AddDays(-800),
                    DateEnded = DateTime.Now.AddDays(-601)
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
                    DescriptionEn = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout." +
                                    " The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here," +
                                    " content here', making it look like readable English.",
                    DescriptionSv = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout." +
                                    " The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here," +
                                    " content here', making it look like readable English.",
                    EmploymentRate = "FullTime",
                    RoleEn = "Software Developer",
                    RoleSv = "Mjukvaruutvecklare",
                    DateStarted = DateTime.Now.AddDays(-400),
                    DateEnded = DateTime.Now.AddDays(-201)
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = users[0].Id,
                    CityEn = "Gothenburg",
                    CitySv = "Göteborg",
                    CompanyName = "Stena Line",
                    CountryEn = "Sweden",
                    CountrySv = "Sverige",
                    DescriptionEn = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout." +
                                    " The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters," +
                                    " as opposed to using 'Content here, content here', making it look like readable English.",
                    DescriptionSv = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout." +
                                    " The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters," +
                                    " as opposed to using 'Content here, content here', making it look like readable English.",
                    EmploymentRate = "PartTime",
                    RoleEn = "Software developer",
                    RoleSv = "Mjukvaruutvecklare",
                    DateStarted = DateTime.Now.AddDays(-200),
                    DateEnded = DateTime.Now.AddDays(-50)
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = users[0].Id,
                    CityEn = "Gothenburg",
                    CitySv = "Göteborg",
                    CompanyName = "FrontEdge IT",
                    CountryEn = "Sweden",
                    CountrySv = "Sverige",
                    DescriptionEn = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout." +
                                    " The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here'," +
                                    " making it look like readable English.",
                    DescriptionSv = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout." +
                                    " The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here'," +
                                    " making it look like readable English.",
                    EmploymentRate = "Internship",
                    RoleEn = "Software developer",
                    RoleSv = "Mjukvaruutvecklare",
                    DateStarted = DateTime.Now.AddDays(-49)
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
            
            //////////////////////////////////
            ///      SEED CERTIFICATES     ///
            //////////////////////////////////
            var certificates = new List<Certificate>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = users[0].Id,
                    Organisation = "Udemy",
                    IdentificationId = "werg0wrg0wuerrg0whrg",
                    ReferenceAddress = "www.google.com",
                    CertificateNameEn = "Javascript for beginners",
                    CertificateNameSv = "Javascript för nybörjare",
                    DateIssued = DateTime.Now.AddDays(-100)
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = users[0].Id,
                    Organisation = "Udemy",
                    IdentificationId = "werg0wrg0wuerrg0whrg2",
                    ReferenceAddress = "www.google.com",
                    CertificateNameEn = "Css for beginners",
                    CertificateNameSv = "Css för nybörjare",
                    DateIssued = DateTime.Now.AddDays(-150)
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = users[0].Id,
                    Organisation = "Udemy",
                    IdentificationId = "werg0wrg0wuerrg0whrg",
                    ReferenceAddress = "www.google.com",
                    CertificateNameEn = "React for beginners",
                    CertificateNameSv = "React för nybörjare",
                    DateIssued = DateTime.Now.AddDays(-200)
                }
            };
            
            modelBuilder.Entity<Certificate>().HasData(certificates);
            
            //////////////////////////////////
            ///         SEED Skills        ///
            //////////////////////////////////
            var skills = new List<Skill>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    SkillName = "Javascript"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    SkillName = "React"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    SkillName = "C#"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    SkillName = "SQL"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    SkillName = "CSS"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    SkillName = "NextJS"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    SkillName = "MySql"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    SkillName = "Typescript"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    SkillName = "Python"
                }
            };
            
            modelBuilder.Entity<Skill>().HasData(skills);

            //////////////////////////////////
            ///         SEED Skills        ///
            //////////////////////////////////
            var userSkills = new List<UserSkill>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    SkillId = skills[0].Id,
                    SkillLevel = 4,
                    UserId = users[0].Id
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    SkillId = skills[1].Id,
                    SkillLevel = 3,
                    UserId = users[0].Id
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    SkillId = skills[2].Id,
                    SkillLevel = 2,
                    UserId = users[0].Id
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    SkillId = skills[3].Id,
                    SkillLevel = 2,
                    UserId = users[0].Id
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    SkillId = skills[4].Id,
                    SkillLevel = 5,
                    UserId = users[0].Id
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    SkillId = skills[5].Id,
                    SkillLevel = 1,
                    UserId = users[0].Id
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    SkillId = skills[6].Id,
                    SkillLevel = 1,
                    UserId = users[0].Id
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    SkillId = skills[7].Id,
                    SkillLevel = 2,
                    UserId = users[0].Id
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    SkillId = skills[8].Id,
                    SkillLevel = 3,
                    UserId = users[0].Id
                }
            };
            
            modelBuilder.Entity<UserSkill>().HasData(userSkills);
        }
    }
}