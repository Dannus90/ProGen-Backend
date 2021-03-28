using System;

namespace Core.Application.Exceptions
{
    public class HttpExceptionResponse : Exception
    {
        public HttpExceptionResponse(int statusCode, string msg) : base(msg)
        {
            StatusCode = statusCode;
        }

        public int StatusCode { get; }
    }
}