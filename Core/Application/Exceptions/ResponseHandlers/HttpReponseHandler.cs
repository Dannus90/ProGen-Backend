using System;

namespace Core.Application.Exceptions.ResponseHandlers
{
    public static class HttpResponseHandler
    {
        public static ExceptionResponse Respond(HttpExceptionResponse e)
        {
            return new(e.StatusCode, e.Message, "HttpResponseException");
        }
    }
}