using System;
using System.Net;

namespace Core.Application.Exceptions
{
    public class HttpExceptionResponse : Exception
    {
        public int StatusCode { get; }

        public HttpExceptionResponse(int statusCode, string msg) : base(msg)
        {
            StatusCode = statusCode;
        }
    }
}