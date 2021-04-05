using System.Text.RegularExpressions;
using Core.Application.Exceptions;
using Core.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace API.helpers
{
    public static class CredentialsValidation
    {
        public static void ValidateCredentials(UserCredentialsWithName userCredentialsWithName)
        {
            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (!regex.Match(userCredentialsWithName.Email).Success || userCredentialsWithName.Password.Length < 10)
                throw new HttpExceptionResponse(StatusCodes.Status400BadRequest,
                    "Invalid email format or password length supplied");
            
            if (userCredentialsWithName.Firstname == "" || userCredentialsWithName.Lastname == "")
                throw new HttpExceptionResponse(StatusCodes.Status400BadRequest,
                    "Firstname and lastname are required.");
        }
    }
}