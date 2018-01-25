namespace CatsWebApp.Controllers
{
    using CatsWebApp.Models;
    using CatsWebApp.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class CatsController : Controller
    {
        private readonly ICatService cats;

        public CatsController(ICatService cats)
        {
            this.cats = cats;
        }

        [Authorize]
        public IActionResult Index()
        {
            Response.Cookies.Append("My - Custom - Cookie", "MVC is cool");

            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Index(CatDetailsModel model)
        {
            //vzimane na usera emaila
            //var email = User.Identity.Name;

            return View();
        }


        // cats/create
        public IActionResult Create() => View();

        //public IActionResult Create() => View();   -> samo ako e ednoredov  red
    }
}
