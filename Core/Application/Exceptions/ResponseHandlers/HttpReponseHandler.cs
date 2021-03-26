using System;

namespace Core.Application.Exceptions.ResponseHandlers
{
    public static class HttpResponseHandler
    {
        public static ExceptionResponse respond(HttpExceptionResponse e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.Source);
            Console.WriteLine(e.Data.Values);
            Console.WriteLine(e.StatusCode);
            Console.WriteLine(e.InnerException);

            return new ExceptionResponse(e.StatusCode, e.Message, "HttpResponseException");
        }
    }
}