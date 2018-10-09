using RunesWebApp.Data;
using RunesWebApp.Services;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.WebServer.Results;
using System.IO;
using System.Runtime.CompilerServices;
using SIS.HTTP.Enums;

namespace RunesWebApp.Controllers
{
    public abstract class BaseController
    {
        // conts
        private const string RootDirectoryPath = "../../../";

        private const string ControllerDefaultName = "Controller";

        private const string DirectorySeperator = "/";

        private const string ViewFolderName = "Views";

        private const string HtmlFileExtension = ".html";

        private string GetCurrentControllerName()
            => this.GetType().Name.Replace(ControllerDefaultName, string.Empty);

        protected BaseController()
        {
            this.Db = new RunesDbContext();
            this.UserCookieService = new UserCookieService();
        }

        protected RunesDbContext Db { get; }

        protected IUserCookieService UserCookieService { get; }

        protected string GetUsername(IHttpRequest request)
        {
            if (!request.Cookies.ContainsCookie(".auth-cakes"))
            {
                return null;
            }

            var cookie = request.Cookies.GetCookie(".auth-cakes");
            var cookieContent = cookie.Value;
            var userName = this.UserCookieService.GetUserData(cookieContent);
            return userName;
        }

        protected IHttpResponse View([CallerMemberName] string viewName = "")
        {
            string filePath = RootDirectoryPath +
                ViewFolderName +
                DirectorySeperator +
                this.GetCurrentControllerName() +
                DirectorySeperator +
                viewName +
                HtmlFileExtension;

            if (!File.Exists(filePath))
            {
                return new BadRequestResult($"View {viewName} not found", HttpResponseStatusCode.NotFound);
            }

            var fileContent = File.ReadAllText(filePath);

            var response = new HtmlResult(fileContent, HttpResponseStatusCode.Ok);

            return response;
        }

    }
}
