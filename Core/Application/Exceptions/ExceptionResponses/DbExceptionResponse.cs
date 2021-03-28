using System;

namespace Core.Application.Exceptions
{
    public class DbExceptionResponse : Exception
    {
        public DbExceptionResponse(int statusCode, string msg) : base(msg)
        {
            StatusCode = statusCode;
        }

        public int StatusCode { get; }
    }
}