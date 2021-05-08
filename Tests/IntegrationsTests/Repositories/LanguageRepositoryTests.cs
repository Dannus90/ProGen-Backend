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
    public class LanguageRepositoryTests
    {
        private readonly IUserAuthRepository _userAuthRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILanguageRepository _languageRepository;
        private string setupEmail;
        private string setupPassword;
        private string firstName;
        private string lastName;
        private Guid setupUserId;

        public LanguageRepositoryTests()
        {
            _userAuthRepository = new UserAuthRepository
                (TestConfig.getTestConnectionString());
            _userRepository = new UserRepository
                (TestConfig.getTestConnectionString());
            _languageRepository = new LanguageRepository
                (TestConfig.getTestConnectionString());
        }

        [OneTimeSetUp]
        public async Task Setup()
        {
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
        public async Task CreateUserLanguage_WithLanguageDataAndUserId_SuccessfullyCreatesUserData()
        {
            // Arrange
            var language = new Language()
            {
                Id = Guid.NewGuid(),
                UserId = setupUserId,
                LanguageEn = "English",
                LanguageSv = "Engelska",
            };
            
            // Act
            var languageId = await _languageRepository.CreateUserLanguage
                (setupUserId.ToString(), language);

            var languageIdAsString = languageId.ToString();

            var retrievedLanguage = await _languageRepository.GetUserLanguage(languageIdAsString);

            // Assert
            Assert.NotNull(retrievedLanguage);
            Assert.AreEqual(retrievedLanguage.LanguageEn.Trim(), language.LanguageEn);
            Assert.AreEqual(retrievedLanguage.LanguageSv.Trim(), language.LanguageSv);
            
            // Cleanup
            await _languageRepository.DeleteUserLanguage
                (languageIdAsString);
        }
    }
}