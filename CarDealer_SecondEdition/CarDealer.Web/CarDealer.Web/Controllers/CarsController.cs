namespace CarDealer.Web.Controllers
{
    using CarDealer.Web.Models.Cars;
    using Microsoft.AspNetCore.Mvc;
    using Services;

    public class CarsController : Controller
    {
        private readonly ICarService _cars;

        public CarsController(ICarService cars)
        {
            this._cars = cars;
        }

        [Route("cars/{make}")]
        public IActionResult ByMake(string make)
        {
            var cars = this._cars.ByMake(make);
            
            return View(new CarsByMakeModel
            {
                Make = make,
                Cars = cars
            });
        }

    }
}
