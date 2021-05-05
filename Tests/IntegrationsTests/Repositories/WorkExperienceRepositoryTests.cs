using System;
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
    
    public class WorkExperienceRepositoryTests
    {
        private string setupEmail;
        private string setupPassword;
        private string firstName;
        private string lastName;
        private Guid setupUserId;
        private readonly IUserAuthRepository _userAuthRepository;
        private readonly IUserRepository _userRepository;
        private readonly IWorkExperienceRepository _workExperienceRepository;

        public WorkExperienceRepositoryTests()
        {
            _userAuthRepository = new UserAuthRepository
                (TestConfig.getTestConnectionString());
            _userRepository = new UserRepository
                (TestConfig.getTestConnectionString());
            _workExperienceRepository = new WorkExperienceRepository(
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
        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
            await _userRepository.DeleteUserByUserId(setupUserId);
        }
        
        [Test]
        public async Task 
            CreateAndDeleteWorkExperience_WithWorkExperienceData_SuccessfullyCreatesAndDeletesWorkExperience()
        {
            // Arrange
            var workExperience = new WorkExperience()
            {
                UserId = setupUserId,
                EmploymentRate = "50%",
                CityEn = "Gothenburg",
                CitySv = "Göteborg",
                CompanyName = "FrontEdge IT",
                CountryEn = "Sweden",
                CountrySv = "Sverige",
                DateStarted = new DateTime(),
                DateEnded = new DateTime(),
                DescriptionEn = "Test description",
                DescriptionSv = "Test beskrivning",
                RoleEn = "Fisher",
                RoleSv = "Fiskare"
            };
            
            // Act
            var workExperienceId = await _workExperienceRepository.CreateWorkExperience
                (workExperience, setupUserId.ToString());

            var retrievedWorkExperience = 
                await _workExperienceRepository.GetWorkExperience(workExperienceId.ToString());

            // Assert
            Assert.IsNotNull(retrievedWorkExperience);
            Assert.AreEqual(retrievedWorkExperience.CityEn.Trim(), workExperience.CityEn);
            Assert.AreEqual(retrievedWorkExperience.CountryEn.Trim(), workExperience.CountryEn);
            
            // Clean up
            await _workExperienceRepository.DeleteWorkExperience(workExperienceId.ToString());
            
            var retrievedWorkExperienceAfterDelete = 
                await _workExperienceRepository.GetWorkExperience(workExperienceId.ToString());
            
            Assert.IsNull(retrievedWorkExperienceAfterDelete);
        }
        
        [Test]
        public async Task 
            CreateAndUpdateWorkExperience_WithNewData_SuccessfullyCreatesAndUpdateWorkExperience()
        {
            // Arrange
            var workExperience = new WorkExperience()
            {
                UserId = setupUserId,
                EmploymentRate = "50%",
                CityEn = "Gothenburg",
                CitySv = "Göteborg",
                CompanyName = "FrontEdge IT",
                CountryEn = "Sweden",
                CountrySv = "Sverige",
                DateStarted = new DateTime(),
                DateEnded = new DateTime(),
                DescriptionEn = "Test description",
                DescriptionSv = "Test beskrivning",
                RoleEn = "Fisher",
                RoleSv = "Fiskare"
            };
            
            // Act
            var workExperienceId = await _workExperienceRepository.CreateWorkExperience
                (workExperience, setupUserId.ToString());
            
            // Arrange
            var workExperienceForUpdate = new WorkExperience()
            {
                UserId = setupUserId,
                EmploymentRate = "50%",
                CityEn = "Gothenburg2",
                CitySv = "Göteborg2",
                CompanyName = "FrontEdge IT2",
                CountryEn = "Sweden2",
                CountrySv = "Sverige2",
                DateStarted = new DateTime(),
                DateEnded = new DateTime(),
                DescriptionEn = "Test description2",
                DescriptionSv = "Test beskrivning2",
                RoleEn = "Fisher",
                RoleSv = "Fiskare"
            };
            
            // Act
            var updatedWorkExperience = 
                await _workExperienceRepository.UpdateWorkExperience(workExperienceId.ToString(), workExperienceForUpdate);

            // Assert
            Assert.IsNotNull(updatedWorkExperience);
            Assert.AreEqual(updatedWorkExperience.CityEn.Trim(), workExperienceForUpdate.CityEn);
            Assert.AreEqual(updatedWorkExperience.CountryEn.Trim(), workExperienceForUpdate.CountryEn);
            
            // Clean up
            await _workExperienceRepository.DeleteWorkExperience(workExperienceId.ToString());
            
            var retrievedWorkExperienceAfterDelete = 
                await _workExperienceRepository.GetWorkExperience(workExperienceId.ToString());
            
            Assert.IsNull(retrievedWorkExperienceAfterDelete);
        }
    }
}
