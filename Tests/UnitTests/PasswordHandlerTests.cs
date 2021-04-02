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
    }
}