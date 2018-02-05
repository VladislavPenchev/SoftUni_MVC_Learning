using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CarDealer.Web.Models;

namespace CarDealer.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }



        public IActionResult Error()
        {
            return View(new ErrorModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
