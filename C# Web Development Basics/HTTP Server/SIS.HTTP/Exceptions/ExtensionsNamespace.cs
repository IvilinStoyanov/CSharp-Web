using System;
using System.Net;

namespace SIS.HTTP.Exceptions
{
   public class ExtensionsNamespace : Exception
    {
        private const string ErrorMessage = "The Server has encountered an error.";

        private const HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

        public ExtensionsNamespace()
            :base(ErrorMessage)
        {

        }
    }
}
