﻿using SIS.Framework.ActionsResults.Base.Contracts;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;

namespace RunesWebApp.Controllers
{
    public class HomeController : BaseController
    {
        //public IHttpResponse Index(IHttpRequest request)
        //{
        //    if (this.IsAuthenticated(request))
        //    {
        //        var username = request.Session.GetParameter("username");
        //        this.ViewBag["username"] = username.ToString();

        //        return this.View("IndexLoggedIn");
        //    }

        //        return this.View();
        //}
        public IActionResult Index(IndexViewModel model)
        {
            return this.View();
        }

        public IActionResult Indexlog(IndexViewModel model)
        {
            return this.View();
        }
    }
}

