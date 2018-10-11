using RunesWebApp.Data;
using RunesWebApp.Services;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.WebServer.Results;
using System.IO;
using System.Runtime.CompilerServices;
using SIS.HTTP.Enums;
using SIS.HTTP.Cookies;
using System.Collections.Generic;

namespace RunesWebApp.Controllers
{
    public abstract class BaseController
    {
        private const string RootDirectoryPath = "../../../";

        private const string ControllerDefaultName = "Controller";

        private const string DirectorySeperator = "/";

        private const string ViewFolderName = "Views";

        private const string HtmlFileExtension = ".html";

        public IDictionary<string, string> ViewBag { get; set; }

        private string GetCurrentControllerName()
            => this.GetType().Name.Replace(ControllerDefaultName, string.Empty);

        protected BaseController()
        {
            this.Db = new RunesDbContext();
            this.UserCookieService = new UserCookieService();
            this.ViewBag = new Dictionary<string, string>();
        }

        protected RunesDbContext Db { get; }

        protected IUserCookieService UserCookieService { get; }

        public bool IsAuthenticated(IHttpRequest request)
        {
            return request.Session.ContainsParameter("username");
        }

        public void SignIn(string username, IHttpRequest request)
        {
            request.Session.AddParameter("username", username);
            var userCookieValue = UserCookieService.GetUserCookie(username);

            request.Cookies.Add(new HttpCookie("IRunes_auth", userCookieValue));
        }

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

            foreach (var viewBagKey in ViewBag.Keys)
            {
                var usernameInter = $"{{{{{viewBagKey}}}}}";

                if (fileContent.Contains(usernameInter))
                    fileContent = fileContent.Replace(usernameInter, this.ViewBag[viewBagKey]);
            }

            var response = new HtmlResult(fileContent, HttpResponseStatusCode.Ok);

            return response;
        }
    }
}
