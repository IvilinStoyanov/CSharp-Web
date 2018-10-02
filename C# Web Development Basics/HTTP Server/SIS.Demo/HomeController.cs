using SIS.HTTP.Enums;
using SIS.HTTP.Responses.Contracts;
using SIS.WebServer.Results;
using System.Net;
using System.Threading;

namespace SIS.Demo
{
    public class HomeController
    {
        public IHttpResponse Index()
        {
            string content = "<h1>Hello, Word!</h1>";

            return new HtmlResult(content, HttpStatusCode.OK);
        }
    }
}
