using SIS.HTTP.Headers;
using SIS.HTTP.Responses;
using System.Net;

namespace SIS.WebServer.Results
{
    public class RedirectResult : HttpResponse
    {
        public RedirectResult(string location)
            :base(HttpStatusCode.SeeOther)
        {
            this.Headers.Add(new HttpHeader(HttpHeader.Location, location));
        }
    }
}
