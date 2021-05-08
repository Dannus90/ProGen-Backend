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
    public class LanguageRepositoryTests
    {
        private readonly IUserAuthRepository _userAuthRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILanguageRepository _languageRepository;
        private string setupEmail;
        private string setupPassword;
        private string firstName;
        private string lastName;
        private List<Language> languages;
        private List<Guid> languageIds;
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
            
            // Arrange
            languages = new List<Language>()
            {
                new()
                {
                    LanguageEn = "English",
                    LanguageSv = "Engelska"
                },
                new()
                {
                    LanguageEn = "German",
                    LanguageSv = "Tyska"
                },
                new()
                {
                    LanguageEn = "Spanish",
                    LanguageSv = "Spanska"
                }
            };
            
            var languageIdFirst = await _languageRepository.CreateUserLanguage
                (setupUserId.ToString(), languages[0]);
            var languageIdSecondary = await _languageRepository.CreateUserLanguage
                (setupUserId.ToString(), languages[1]);
            var languageIdTertiary = await _languageRepository.CreateUserLanguage
                (setupUserId.ToString(), languages[2]);

            languageIds = new List<Guid>();
            
            languageIds.AddRange(new List<Guid>() 
                { languageIdFirst, languageIdSecondary, languageIdTertiary });
        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
            await _userRepository.DeleteUserByUserId(setupUserId);

            foreach (var languageId in languageIds)
            {
                await _languageRepository.DeleteUserLanguage(languageId.ToString());
            }
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
        
        [Test]
        public async Task UpdateUserLanguage_WithLanguageDataAndLanguageId_SuccessfullyUpdatesUserLanguage()
        {
            // Arrange
            var language = new Language()
            {
                Id = Guid.NewGuid(),
                UserId = setupUserId,
                LanguageEn = "English",
                LanguageSv = "Engelska",
            };

            var languageForUpdate = new Language()
            {
                LanguageEn = "English2",
                LanguageSv = "Engelska2"
            };
            
            // Act
            var languageId = await _languageRepository.CreateUserLanguage
                (setupUserId.ToString(), language);
            
            var languageIdAsString = languageId.ToString();

            await _languageRepository.UpdateUserLanguage(languageIdAsString, languageForUpdate);

            var retrievedLanguage = await _languageRepository.GetUserLanguage(languageIdAsString);

            // Assert
            Assert.NotNull(retrievedLanguage);
            Assert.AreEqual(retrievedLanguage.LanguageEn.Trim(), languageForUpdate.LanguageEn);
            Assert.AreEqual(retrievedLanguage.LanguageSv.Trim(), languageForUpdate.LanguageSv);
            
            // Cleanup
            await _languageRepository.DeleteUserLanguage
                (languageIdAsString);
        }
        
        [Test]
        public async Task GetUserLanguages_GetsAllUserLanguagesByUserId_SuccessfullyGetsUserLanguages()
        {
            // Act
            var languages = await _languageRepository.GetUserLanguages
                (setupUserId.ToString());
            
            // Assert
            Assert.NotNull(languages);
            Assert.AreEqual(languages.Count(), 3);
        }
    }
}