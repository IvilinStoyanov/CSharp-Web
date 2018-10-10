using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace RunesWebApp.Controllers
{
    public class UsersController : BaseController
    {
        public IHttpResponse Login(IHttpRequest request)
            => this.View();

        public IHttpResponse Register(IHttpRequest request)
            => this.View();
    }
}
