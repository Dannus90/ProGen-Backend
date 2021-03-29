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
        private string setupEmail;
        private string setupPassword;
        private Guid setupUserId;

        public UserAuthRepositoryTests()
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
        
        [Test]
        public async Task SaveRefreshToken_WithRefreshTokenAndUserId_SuccessfullySavesRefreshToken()
        {
            // Arrange
            const string refreshToken = "randomTestToken";

            
            // Act
            await _userAuthRepository.SaveRefreshToken(refreshToken, setupUserId);
            var retrievedRefreshToken = await _userAuthRepository.GetRefreshTokenByUserId(setupUserId.ToString());
            
            // Assert
            Assert.AreEqual(retrievedRefreshToken.Token, refreshToken);
            Assert.AreEqual(retrievedRefreshToken.UserId, setupUserId);
            
            // Clean up
            await _userAuthRepository.DeleteRefreshTokenByUserId(setupUserId.ToString());
            var removedRefreshToken = await _userAuthRepository.
                GetRefreshTokenByUserId(setupUserId.ToString());

            // Assert clean up
            Assert.IsNull(removedRefreshToken);
        }
        
        [Test]
        public async Task UpdateRefreshToken_WithRefreshTokenAndUserId_SuccessfullySavesRefreshToken()
        {
            // Arrange
            const string refreshToken = "randomTestToken";
            const string updatedRefreshToken = "newRefreshToken";

            // Act
            await _userAuthRepository.SaveRefreshToken(refreshToken, setupUserId);
            var retrievedRefreshToken = await _userAuthRepository
                .GetRefreshTokenByUserId(setupUserId.ToString());

            await _userAuthRepository.UpdateRefreshTokenByUserId(updatedRefreshToken, setupUserId);
            var retrievedUpdatedRefreshToken = await _userAuthRepository
                .GetRefreshTokenByUserId(setupUserId.ToString());
            
            // Assert
            Assert.AreNotEqual(retrievedRefreshToken.Token, retrievedUpdatedRefreshToken.Token);

            // Clean up
            await _userAuthRepository.DeleteRefreshTokenByUserId(setupUserId.ToString());
            var removedRefreshToken = await _userAuthRepository.
                GetRefreshTokenByUserId(setupUserId.ToString());
            
            // Assert clean up
            Assert.IsNull(removedRefreshToken);
        }
    }
}