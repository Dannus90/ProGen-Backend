using System;
using Microsoft.AspNetCore.Http;
using Npgsql;

namespace Core.Application.Exceptions.ResponseHandlers
{
    public static class DbResponseHandler
    {
        public static ExceptionResponse Respond(NpgsqlException e)
        {
            switch (e.SqlState)
            {
                case "23505":
                    return GetDublicateKeyType(e);
                
                default: 
                    Console.WriteLine($"[{e.GetType().FullName}] {e.Message}");
                    Console.WriteLine(e.StackTrace);
                    Console.WriteLine(e.InnerException?.StackTrace);
                    return DefaultExceptionResponse();
            }
        }
        
        /**
         * The default fallback exception.
         */
        private static ExceptionResponse DefaultExceptionResponse()
        {
            return new(
                StatusCodes.Status500InternalServerError,
                "Unspecified error",
                ExceptionTypes.DatabaseError
            );
        }

        private static ExceptionResponse GetDublicateKeyType(NpgsqlException e)
        {
            switch (e.Message)
            {
                case string msg when msg.Contains("IX_user_base_email"):
                    return new ExceptionResponse(409, "Email already in use.",
                        "DuplicateKeyError");
                
                default:
                    return new ExceptionResponse(409, "Duplicate key",
                        "DuplicateKeyError");
            }
        }
    }
}