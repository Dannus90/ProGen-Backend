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
            ExceptionResponse response = null;
            Console.WriteLine(exception.GetType());
            switch (exception)
            {
                case HttpExceptionResponse e:
                    response = HttpResponseHandler.respond(e);
                    break;
            }
            
            context.ExceptionHandled = true;
            context.Result = response.CreateObjectResult();
        }

        private static ExceptionResponse DefaultExceptionResponse()
        {
            return new ExceptionResponse
            {
                Message = "Internal Server Error",
                StatusCode = StatusCodes.Status500InternalServerError,
                
            }
        }
    }
}