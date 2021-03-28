using System;
using System.Threading.Tasks;
using Infrastructure.Identity.Repositories;
using Infrastructure.Identity.Repositories.Interfaces;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Repositories.Interfaces;
using Infrastructure.Security;
using NUnit.Framework;

namespace Tests.IntegrationsTests
{
    public class UserAuthRepositoryTests
    {
        private readonly IUserAuthRepository _userAuthRepository;
        private readonly IUserRepository _userRepository;

        public UserAuthRepositoryTests()
        {
            _userAuthRepository = new UserAuthRepository(TestConfig.ConnectionString);
            _userRepository = new UserRepository(TestConfig.ConnectionString);
        }

        [Test]
        public async Task RegisterUser_WithEmailAndPassword_SuccessfullyRegistersUser()
        {
            // Arrange
            const string password = "dfgjwasgasgqwqer";
            const string email = "farmorAnka@gmail.com";
            var hashedPassword = PasswordHandler.HashPassword(password);
            
            // Act
            await _userAuthRepository.RegisterUser(hashedPassword, email);
            var user = await _userRepository.GetUserByEmail(email);
            
            // Assert
            Assert.AreEqual(user.Email.Trim(), email);
            Assert.AreEqual(user.Password.Trim(), hashedPassword);
            
            // Clean up
            await _userRepository.DeleteUserByUserId(user.Id);
        }
        
        [Test]
        public async Task UpdatesLastLoggedIn_WithNewTime_SuccessfullyUpdatesLastLoggedIn()
        {
            // Arrange
            const string password = "dfgjwasgasgqwqer";
            const string email = "farmorAnka@gmail.com";
            var hashedPassword = PasswordHandler.HashPassword(password);
            
            // Act
            await _userAuthRepository.RegisterUser(hashedPassword, email);
            var user = await _userRepository.GetUserByEmail(email);

            await _userAuthRepository.UpdateLastLoggedIn(user.Id);

            var updatedUser = await _userRepository.GetUserByEmail(email);
            
            // Assert
            Assert.NotNull(updatedUser.LastLogin);
            Assert.AreNotEqual(updatedUser.LastLogin, user.LastLogin);
            
            // Clean up
            await _userRepository.DeleteUserByUserId(user.Id);
        }
    }
}