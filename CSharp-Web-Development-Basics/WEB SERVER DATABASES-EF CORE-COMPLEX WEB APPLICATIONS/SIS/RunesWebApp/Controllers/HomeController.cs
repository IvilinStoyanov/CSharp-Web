using SIS.HTTP.Enums;
using SIS.HTTP.Responses;
using SIS.WebServer.Results;

namespace RunesWebApp.Controllers
{
    public class HomeController
    {
        public IHttpResponse Index()
        {
            string content = "<h1>Hello,Just for the test Word!</h1>";

            return new HtmlResult(content, HttpResponseStatusCode.Ok);
        }

    }
}
