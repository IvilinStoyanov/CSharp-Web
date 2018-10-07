using CakesWebApp.Models;
using CakesWebApp.Services;
using CakesWebApp.Services.Contracts;
using SIS.HTTP.Cookies;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;
using SIS.WebServer.Results;
using System;
using System.Linq;

namespace CakesWebApp.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IHashService hashService;
        private readonly IUserCookieService userCookieService;

        public AccountController()
        {
            this.hashService = new HashService();
            this.userCookieService = new UserCookieService();
        }

        public IHttpResponse Register(IHttpRequest request)
        {
            return this.View("Register");
        }

        public IHttpResponse DoRegister(IHttpRequest request)
        {
            // Read input from form fields
            var username = request.FormData["username"].ToString().Trim();
            var password = request.FormData["password"].ToString();
            var confirmPassword = request.FormData["confirmPassword"].ToString();

            // 1. Validate Data Input

            // Username validation   
            if (string.IsNullOrEmpty(username) || username.Length < 4)
            {
                return this.BadRequestError("Please enter a username with at least 4 or more symbols");
            }

            if (Db.Users.Any(x => x.Username == username))
            {
                return this.BadRequestError("Username already exist");
            }
            // Password validation
            if (string.IsNullOrEmpty(password) || password.Length < 6)
            {
                return this.BadRequestError("Please enter a password with at leats 6 or more symbols");
            }

            if (password != confirmPassword)
            {
                return this.BadRequestError("Passwords do not match");
            }

            // 2. Encrypt password using sha256 algorithm
            var hashedPassword = this.hashService.Hash(password);

            // 3. Create user
            var user = new User
            {
                Name = username,
                Username = username,
                Password = hashedPassword
            };

            this.Db.Users.Add(user);

            try
            {
                this.Db.SaveChanges();
            }
            catch (Exception e)
            {
                // TODO: Log error
                return this.ServerError(e.Message);
            }

            // 4. Redirect to home page
            return new RedirectResult("/");
        }

        public IHttpResponse Login(IHttpRequest request)
        {
            return this.View("Login");
        }

        public IHttpResponse DoLogin(IHttpRequest request)
        {
            // 1. Validate user
            var username = request.FormData["username"].ToString().Trim();
            var password = request.FormData["password"].ToString();

            var hashedPassword = this.hashService.Hash(password);

            var user = this.Db.Users.FirstOrDefault(x => x.Username == username &&
            x.Password == hashedPassword);

            if (user == null)
            {
                return this.BadRequestError("Invalid username or password");
            }

            // 2. Get cookie/Session
            var cookieContent = this.userCookieService.GetUserCookie(user.Username);
            
            //  3. Redirect

            var response = new RedirectResult("/");
            response.Cookies.Add(new HttpCookie(".auth-cakes", cookieContent, 7));

            return response;

        }
    }
}
