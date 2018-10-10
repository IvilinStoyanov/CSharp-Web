using SIS.HTTP.Requests;
using SIS.HTTP.Responses;

namespace RunesWebApp.Controllers
{
    public class HomeController : BaseController
    {
        public IHttpResponse Index(IHttpRequest request)
             => this.View();
    }
}

