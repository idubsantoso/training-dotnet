using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApi.Exceptions
{
    public class CustomException : Exception
    {
        //testing
        public List<string>? ErrorMessages { get; }

        public HttpStatusCode StatusCode { get; }

        public CustomException(string message, List<string>? errors = default, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
            : base(message)
        {
            ErrorMessages = errors;
            StatusCode = statusCode;
        }
    }

    public class Flash
    {
        public required string Message { get; set; }
        public required Dictionary<string, List<string>> Errors { get; set; }
        public int Status { get; set; }
    }
}