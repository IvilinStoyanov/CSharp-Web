using System;
using System.Net;

namespace SIS.HTTP.Exceptions
{
    public class BadRequestException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
    }
}
