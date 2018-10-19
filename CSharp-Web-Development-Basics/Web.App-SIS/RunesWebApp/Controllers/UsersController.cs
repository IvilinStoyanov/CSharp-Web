using SIS.Framework.Controllers;
using RunesWebApp.Services.Contracts;
using SIS.Framework.ActionsResults.Base.Contracts;
using SIS.Framework.Attributes.Methods;
using RunesWebApp.ViewModels;
using RunesWebApp.Services;

namespace RunesWebApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult Login() => this.View();
        public IActionResult Register() => this.View();
    
        [HttpPost]
        public IActionResult Register(LoginViewModel model)
        {
            if (!ModelState.IsValid.HasValue || !ModelState.IsValid.Value)
            {
                return this.RedirectToAction("/users/register");
            }
            var user = this.usersService.CreateUser(
                model.Username,
                model.Password,
                model.Password);

            return this.RedirectToAction("/home/index");          
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid.HasValue || !ModelState.IsValid.Value)
            {
                return this.RedirectToAction("/users/login");
            }

            var userExists = this.usersService
                .ExistsByUsernameAndPassword(
                    model.Username,
                    model.Password);

            if (!userExists)
            {
                return this.RedirectToAction("/users/login");
            }

            this.Request.Session.AddParameter("username", model.Username);
            

            return this.RedirectToAction("/home/indexlog");
        }
    }
}



