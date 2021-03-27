using System;
using System.Data.Common;
using Core.Application.Exceptions.ResponseHandlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Npgsql;

namespace Core.Application.Exceptions
{
    public class ExceptionFilter : IExceptionFilter
    {
        /**
         * Called when an exception is thrown. Configured through startup.cs.
         */
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            ExceptionResponse response;
            switch (exception)
            {
                case NpgsqlException e:
                    response = DbResponseHandler.Respond(e);
                    break;
                
                case HttpExceptionResponse e:
                    response = HttpResponseHandler.Respond(e);
                    break;

                default:
                    Console.WriteLine($"[{exception.GetType().FullName}] {exception.Message}");
                    Console.WriteLine(exception.StackTrace);
                    Console.WriteLine(exception.InnerException?.StackTrace);
                    response = DefaultExceptionResponse();
                    break;
            }

            context.ExceptionHandled = true;
            context.Result = response.CreateObjectResult();
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
    }
}