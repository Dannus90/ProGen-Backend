using System;
using System.Text.RegularExpressions;

namespace API.helpers
{
    public class CredentialsValidation
    {
        public static void ValidateCredentials(string password, string email)
        {
            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (!regex.Match(email).Success || password.Length < 10)
            {
                throw new Exception("Please provide proper credentials.");
            }
        }
    }
}