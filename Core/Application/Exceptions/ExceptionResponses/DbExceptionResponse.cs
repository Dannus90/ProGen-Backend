using System;
using System.Data.Common;

namespace Core.Application.Exceptions
{
    public class DbExceptionResponse : Exception
        {
           public int StatusCode { get; }

            public DbExceptionResponse(int statusCode, string msg) : base(msg)
            {
                StatusCode = statusCode;
            }
        }
}