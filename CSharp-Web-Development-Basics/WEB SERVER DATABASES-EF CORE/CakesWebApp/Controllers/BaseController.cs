using CakesWebApp.Data;
using SIS.HTTP.Responses.Contracts;
using SIS.WebServer.Results;
using System.IO;
using System.Net;

namespace CakesWebApp.Controllers
{
    public abstract class BaseController
    {
        protected CakesDbContext Db { get; }

        protected BaseController()
        {
            this.Db = new CakesDbContext();
        }

      protected IHttpResponse View(string viewName)
        {
            var content = File.ReadAllText("Views/" + viewName + ".html");

            return new HtmlResult(content, HttpStatusCode.OK);
        }

        protected IHttpResponse BadRequestError(string errorMessage)
        {
            return new HtmlResult($"<h1>{errorMessage}</h1>", HttpStatusCode.BadRequest);
        }

        protected IHttpResponse ServerError(string errorMessage)
        {
            return new HtmlResult($"<h1>{errorMessage}</h1>", HttpStatusCode.InternalServerError);
        }
    }
}
