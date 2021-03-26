using System;
using System.Net;
using Core.Application.Exceptions.ResponseHandlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Core.Application.Exceptions
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            Exception exception = context.Exception;
            ExceptionResponse response;
            switch (exception)
            {
                case HttpExceptionResponse e:
                    response = HttpResponseHandler.respond(e);
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

        private static ExceptionResponse DefaultExceptionResponse()
        {
            return new ExceptionResponse(
                StatusCodes.Status500InternalServerError,
                "Unspecified error",
                ExceptionTypes.DatabaseError
            );
        }
    }
}