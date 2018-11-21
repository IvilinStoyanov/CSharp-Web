using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Pepper.Data.Web.Data;
using Pepper.Web.Models;
using Pepper.Web.ViewModels.Home;

namespace Pepper.Web.Controllers
{
    public class HomeController : Controller
    {
        private PepperDbContext dbContext;

        public HomeController(PepperDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var viewModel = new LoggedInViewModel
                {
                    Products = dbContext.Products.Select(p =>
                        new ProductIndexDto
                        {
                            Id = p.Id,
                            Name = p.Name,
                            ShortDescription = p.Description.Substring(0, 37) + "...",
                            Price = p.Price
                        }).ToList()
                };

                return this.View("IndexLoggedIn", viewModel);
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}