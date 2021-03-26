using System;
using System.Text.RegularExpressions;
using Core.Application.Exceptions;
using Microsoft.AspNetCore.Http;


namespace API.helpers
{
    public static class CredentialsValidation
    {
        public static void ValidateCredentials(string password, string email)
        {
            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (!regex.Match(email).Success || password.Length < 10)
            {
                throw new HttpExceptionResponse(StatusCodes.Status400BadRequest, 
                    "Invalid email format or password length supplied");
            }
        }
    }
}