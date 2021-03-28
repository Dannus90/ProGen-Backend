using Microsoft.AspNetCore.Mvc;

namespace Core.Application.Exceptions
{
    public class ExceptionResponse
    {
        public ExceptionResponse(int statusCode, string message, string type)
        {
            StatusCode = statusCode;
            Message = message;
            Type = type;
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }

        public ObjectResult CreateObjectResult()
        {
            return new(this)
            {
                StatusCode = StatusCode
            };
        }
    }
}