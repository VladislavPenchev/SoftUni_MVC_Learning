namespace CatsWebApp.Controllers
{
    using CatsWebApp.Models;
    using Microsoft.AspNetCore.Mvc;

    public class CatsController : Controller
    {
        public IActionResult Index() => View();

        [HttpPost]
        public IActionResult Index(CatDetailsModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home ");
            }
            return View(model);
        }


        // cats/create
        public IActionResult Create() => View();

        //public IActionResult Create() => View();   -> samo ako e ednoredov  red
    }
}
