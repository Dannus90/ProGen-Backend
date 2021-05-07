using API.helpers;
using Core.Application.Exceptions;
using Core.Domain.Models;
using Infrastructure.Security;
using NUnit.Framework;

namespace Tests.UnitTests
{
    public class PasswordHandlerTests
    {
        [Test]
        [TestCase("Password123")]
        [TestCase("password456")]
        [TestCase("pass1word789")]
        public void HashPassword_WithBcrypt_ThePasswordIdHashedCorrectly(string testPassword)
        {
            var hashedHashedPassword = PasswordHandler.HashPassword(testPassword);
            var outcome = PasswordHandler.VerifyPassword(testPassword
                , hashedHashedPassword);

            Assert.AreNotEqual(hashedHashedPassword, testPassword);
            Assert.AreEqual(true, outcome);
        }

        [Test]
        [TestCase("Password123")]
        [TestCase("password456")]
        [TestCase("pass1word789")]
        public void HashPassword_CompareWithWrongPassword_TheVerificationFails(string testPassword)
        {
            var hashedHashedPassword = PasswordHandler.HashPassword(testPassword);
            var invalidPassword = "invalidpassword123";
            var outcome = PasswordHandler.VerifyPassword(invalidPassword
                , hashedHashedPassword);

            Assert.AreEqual(false, outcome);
        }
        
        [Test]
        [TestCase("failmail/gmail.com")]
        [TestCase("failmail@gmail")]
        [TestCase("failmail.com")]
        public void ValidateEmailChange_GivesInvalidEmail_ShouldThrowHttpResponseException(string email)
        {
            // Arrange
            var emailChangeModel = new ChangeEmailModel()
            {
                Password = "trallala",
                NewEmail = email
            };
            
            // Assert
            Assert.Throws<HttpExceptionResponse>(delegate
            {
                CredentialsValidation.ValidateEmailChange(emailChangeModel);
            });
        }
        
        [Test]
        [TestCase("persson.daniel.1990@gmail.com")]
        [TestCase("daniel@gmail.com")]
        [TestCase("persson.daniel.1990@gmail.se")]
        public void ValidateEmailChange_GivesValidEmail_ShouldNotThrowHttpResponseException(string email)
        {
            // Arrange
            var emailChangeModel = new ChangeEmailModel()
            {
                Password = "trallala",
                NewEmail = email
            };
            
            // Assert
            Assert.DoesNotThrow(delegate
            {
                CredentialsValidation.ValidateEmailChange(emailChangeModel);
            });
        }
        
        [Test]
        [TestCase("f25422345")]
        [TestCase("23425")]
        [TestCase("23452345")]
        public void ValidatePasswordLength_GivesInvalidPassword_ShouldThrowHttpResponseException(string password)
        {
            // Assert
            Assert.Throws<HttpExceptionResponse>(delegate
            {
                CredentialsValidation.ValidatePasswordLength(password);
            });
        }
        
        [Test]
        [TestCase("f25422345234")]
        [TestCase("23425234234234")]
        [TestCase("2345234523423422")]
        public void ValidatePasswordLength_GivesValidPassword_ShouldNotThrowHttpResponseException(string password)
        {
            // Assert
            Assert.DoesNotThrow(delegate
            {
                CredentialsValidation.ValidatePasswordLength(password);
            });
        }
    }
}