using System;
using System.Threading.Tasks;
using AutoMapper;
using Core.Domain.Models;
using Core.Mapping;
using Infrastructure.Identity.Repositories;
using Infrastructure.Identity.Repositories.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Repositories.Interfaces;
using Infrastructure.Security;
using NUnit.Framework;

namespace Tests.IntegrationsTests.Repositories
{
    public class FullCvInformationRepositoryTests
    {
        private readonly IUserAuthRepository _userAuthRepository;
        private readonly IUserRepository _userRepository;
        private readonly IFullCvInformationRepository _fullCvInformationRepository;
        private string setupEmail;
        private string setupPassword;
        private string firstName;
        private string lastName;
        private Guid setupUserId;
        
        private readonly IMapper mapper = new MapperConfiguration(mc => 
            { mc.AddProfile(new SimpleMappers()); }).CreateMapper();
        
        public FullCvInformationRepositoryTests()
        {
            _userAuthRepository = new UserAuthRepository
                (TestConfig.getTestConnectionString());
            _userRepository = new UserRepository
                (TestConfig.getTestConnectionString());
            _fullCvInformationRepository = new FullCvInformationRepository
                (TestConfig.getTestConnectionString(), mapper);
        }
        
        [OneTimeSetUp]
        public async Task Setup()
        {
            // Arrange
            setupPassword = "dfgjwasgasgqwqer";
            setupEmail = "farbrorAnka2@gmail.com";
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
        public async Task GetFullCvInformation_ByUserId_SuccessfullyGetsFullCvInformation()
        {
            // Act
            var fullCvInformation = await _fullCvInformationRepository.
                GetFullCvInformation(setupUserId.ToString());

            // Assert
            Assert.NotNull(fullCvInformation);
            Assert.AreEqual
                (fullCvInformation.FullUserInformationDto.User.FirstName.Trim(), "Daniel");
            Assert.AreEqual
                (fullCvInformation.FullUserInformationDto.User.LastName.Trim(), "Persson");
            Assert.AreEqual
                (fullCvInformation.FullUserInformationDto.User.Email.Trim(), "farbrorAnka2@gmail.com");
        }
    }
}