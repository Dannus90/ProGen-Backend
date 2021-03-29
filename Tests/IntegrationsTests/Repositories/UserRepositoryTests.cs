using System;
using System.Threading.Tasks;
using Infrastructure.Identity.Repositories;
using Infrastructure.Identity.Repositories.Interfaces;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Repositories.Interfaces;
using Infrastructure.Security;
using NUnit.Framework;

namespace Tests.IntegrationsTests.Repositories
{
    public class UserRepositoryTests
    {
        private readonly IUserAuthRepository _userAuthRepository;
        private readonly IUserRepository _userRepository;
        private string setupEmail;
        private string setupPassword;
        private Guid setupUserId;

        public UserRepositoryTests()
        {
            _userAuthRepository = new UserAuthRepository(TestConfig.ConnectionString);
            _userRepository = new UserRepository(TestConfig.ConnectionString);
        }

        [OneTimeSetUp]
        public async Task Setup()
        {
            setupPassword = "dfgjwasgasgqwqer";
            setupEmail = "farbrorAnka@gmail.com";
            var hashedPassword = PasswordHandler.HashPassword(setupPassword);
            await _userAuthRepository.RegisterUser(hashedPassword, setupEmail);
            var user = await _userRepository.GetUserByEmail(setupEmail);

            setupUserId = user.Id;
        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
            await _userRepository.DeleteUserByUserId(setupUserId);
        }

        [Test]
        public async Task GetUserByUserId_WithId_SuccessfullyGetsUser()
        {
            // Act
            var user = await _userRepository.GetUserByUserId(setupUserId.ToString());
            
            // Assert
            Assert.AreEqual(user.Email.Trim(), setupEmail);
            Assert.AreEqual(user.Id, setupUserId);
        }
    }
}