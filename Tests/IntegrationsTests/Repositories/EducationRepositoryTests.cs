using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain.DbModels;
using Core.Domain.Models;
using Infrastructure.Identity.Repositories;
using Infrastructure.Identity.Repositories.Interfaces;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Repositories.Interfaces;
using Infrastructure.Security;
using NUnit.Framework;

namespace Tests.IntegrationsTests.Repositories
{
    
    public class EducationRepositoryTests
    {
        private string setupEmail;
        private string setupPassword;
        private string firstName;
        private string lastName;
        private Education education;
        private Guid setupUserId;
        private List<Education> educations;
        private List<Guid> educationIds;
        private readonly IUserAuthRepository _userAuthRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEducationRepository _educationRepository;

        public EducationRepositoryTests()
        {
            _userAuthRepository = new UserAuthRepository
                (TestConfig.getTestConnectionString());
            _userRepository = new UserRepository
                (TestConfig.getTestConnectionString());
            _educationRepository = new EducationRepository(
                    TestConfig.getTestConnectionString());
        }
        
        [OneTimeSetUp]
        public async Task Setup()
        {
            setupPassword = "dfgjwasgasgqwqer";
            setupEmail = "farbrorAnka25@gmail.com";
            firstName = "Daniel";
            lastName = "Persson";
            
            var hashedPassword = PasswordHandler.HashPassword(setupPassword);
            var userCredentialsWithName = new UserCredentialsWithName()
            {
                Email = setupEmail,
                Password = hashedPassword,
                FirstName = firstName,
                LastName = lastName
            };
            
            await _userAuthRepository.RegisterUser(userCredentialsWithName);
            var user = await _userRepository.GetUserByEmail(setupEmail);

            setupUserId = user.Id;
            
            // Arrange
            education = new Education()
            {
                UserId = setupUserId,
                Grade = "VG",
                CityEn = "Gothenburg",
                CitySv = "Göteborg",
                EducationNameEn = "TestEducaton",
                EducationNameSv = "Testutbildning",
                ExamNameEn = "TestExam",
                ExamNameSv = "TestExamenSv",
                SubjectAreaEn = "Testing...",
                SubjectAreaSv = "Testar",
                CountryEn = "Sweden",
                CountrySv = "Sverige",
                DateStarted = new DateTime(),
                DateEnded = new DateTime(),
                DescriptionEn = "Test description",
                DescriptionSv = "Test beskrivning"
            };

            educations = new List<Education>()
            {
                new()
                {
                    Grade = "VG",
                    CityEn = "Gothenburg",
                    CitySv = "Göteborg",
                    EducationNameEn = "TestEducaton",
                    EducationNameSv = "Testutbildning",
                    ExamNameEn = "TestExam",
                    ExamNameSv = "TestExamenSv",
                    SubjectAreaEn = "Testing...",
                    SubjectAreaSv = "Testar",
                    CountryEn = "Sweden",
                    CountrySv = "Sverige",
                    DateStarted = new DateTime(),
                    DateEnded = new DateTime(),
                    DescriptionEn = "Test description",
                    DescriptionSv = "Test beskrivning"
                },
                new()
                {
                    Grade = "VG",
                    CityEn = "Gothenburg",
                    CitySv = "Göteborg",
                    EducationNameEn = "TestEducaton",
                    EducationNameSv = "Testutbildning",
                    ExamNameEn = "TestExam",
                    ExamNameSv = "TestExamenSv",
                    SubjectAreaEn = "Testing...",
                    SubjectAreaSv = "Testar",
                    CountryEn = "Sweden",
                    CountrySv = "Sverige",
                    DateStarted = new DateTime(),
                    DateEnded = new DateTime(),
                    DescriptionEn = "Test description",
                    DescriptionSv = "Test beskrivning"
                },
                new()
                {
                    UserId = setupUserId,
                    Grade = "VG",
                    CityEn = "Gothenburg",
                    CitySv = "Göteborg",
                    EducationNameEn = "TestEducaton",
                    EducationNameSv = "Testutbildning",
                    ExamNameEn = "TestExam",
                    ExamNameSv = "TestExamenSv",
                    SubjectAreaEn = "Testing...",
                    SubjectAreaSv = "Testar",
                    CountryEn = "Sweden",
                    CountrySv = "Sverige",
                    DateStarted = new DateTime(),
                    DateEnded = new DateTime(),
                    DescriptionEn = "Test description",
                    DescriptionSv = "Test beskrivning"
                }
            };
            
            var educationIdFirst = await _educationRepository.CreateEducation
                (educations[0], setupUserId.ToString());
            var educationIdSecondary = await _educationRepository.CreateEducation
                (educations[1], setupUserId.ToString());
            var educationIdTertiary = await _educationRepository.CreateEducation
                (educations[2], setupUserId.ToString());

            educationIds = new List<Guid>();
            
            educationIds.AddRange(new List<Guid>() 
                { educationIdFirst, educationIdSecondary, educationIdTertiary });
        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
            await _userRepository.DeleteUserByUserId(setupUserId);
            foreach (var educationId in educationIds)
            {
                await _educationRepository.DeleteEducation(educationId.ToString(), setupUserId.ToString());
            }
        }
        
        [Test]
        public async Task 
            CreateAndDeleteEducation_WithEducationData_SuccessfullyCreatesAndDeletesEducation()
        {
            // Act
            var educationId = await _educationRepository.CreateEducation
                (education, setupUserId.ToString());

            var retrievedEducation = 
                await _educationRepository.GetEducation(educationId.ToString(), setupUserId.ToString());

            // Assert
            Assert.IsNotNull(retrievedEducation);
            Assert.AreEqual(retrievedEducation.CityEn.Trim(), education.CityEn);
            Assert.AreEqual(retrievedEducation.CountryEn.Trim(), education.CountryEn);
            
            // Clean up
            await _educationRepository.DeleteEducation(educationId.ToString(), setupUserId.ToString());
            
            var retrievedEducationAfterDelete = 
                await _educationRepository.GetEducation(educationId.ToString(), setupUserId.ToString());
            
            Assert.IsNull(retrievedEducationAfterDelete);
        }
        
        [Test]
        public async Task 
            CreateAndUpdateEducation_WithNewData_SuccessfullyCreatesAndUpdateEducation()
        {
            // Act
            var educationId = await _educationRepository.CreateEducation
                (education, setupUserId.ToString());
            
            // Arrange
            var educationForUpdate = new Education()
            {
                UserId = setupUserId,
                Grade = "VG",
                CityEn = "Gothenburg2",
                CitySv = "Göteborg2",
                EducationNameEn = "TestEducaton2",
                EducationNameSv = "Testutbildning2",
                ExamNameEn = "TestExam2",
                ExamNameSv = "TestExamenSv",
                SubjectAreaEn = "Testing...2",
                SubjectAreaSv = "Testar2",
                CountryEn = "Sweden2",
                CountrySv = "Sverige2",
                DateStarted = new DateTime(),
                DateEnded = new DateTime(),
                DescriptionEn = "Test descriptio2",
                DescriptionSv = "Test beskrivning2"
            };
            
            // Act
            var updatedEducation = 
                await _educationRepository.UpdateEducation
                    (educationId.ToString(), educationForUpdate, setupUserId.ToString());

            // Assert
            Assert.IsNotNull(updatedEducation);
            Assert.AreEqual(updatedEducation.CityEn.Trim(), educationForUpdate.CityEn);
            Assert.AreEqual(updatedEducation.CountryEn.Trim(), educationForUpdate.CountryEn);
            
            // Clean up
            await _educationRepository.DeleteEducation(educationId.ToString(), setupUserId.ToString());
            
            var retrievedEducationAfterDelete = 
                await _educationRepository.GetEducation(educationId.ToString(), setupUserId.ToString());
            
            Assert.IsNull(retrievedEducationAfterDelete);
        }
        
        [Test]
        public async Task 
            GetAllEducations_ByUserId_SuccessfullyGetsAllEducation()
        {
            // Act
            var educations = await _educationRepository.GetEducations(setupUserId.ToString());

            // Assert
            Assert.IsNotNull(educations);
            Assert.AreEqual(educations.Count(), 3);
        }
    }
}
