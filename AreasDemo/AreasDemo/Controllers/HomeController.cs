namespace AreasDemo.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using AreasDemo.Models;
    using AreasDemo.Data;
    using System.Linq;
    using AutoMapper.QueryableExtensions;

    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            this._db = db;
        }

        public IActionResult Index()
        {
            //var users = this._db
            //    .Users
            //    .Select(u => new UserViewModel
            //    {
            //        Id = u.Id,
            //        UserName = u.UserName
            //    })
            //    .ToList();

            var users = this._db
                .Users
                .ProjectTo<UserViewModel>()
                .ToList();

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
