namespace CatsWebApp.Controllers
{
    using CatsWebApp.Models;
    using Microsoft.AspNetCore.Mvc;

    public class CatsController : Controller
    {
        public IActionResult Index()
        {
            var model = new CatDetailsModel
            {
                Id = 1,
                Name = "Vankata"
            };

            return View(model);
        }
        
        // cats/create
        public IActionResult Create(int? id)
        {
            return View();
        }

        //public IActionResult Create() => View();   -> samo ako e ednoredov  red
    }
}
