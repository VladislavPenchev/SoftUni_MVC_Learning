namespace CarDealer.Web.Controllers
{
    using CarDealer.Web.Models.Cars;
    using Microsoft.AspNetCore.Mvc;
    using Services;

    [Route("cars")]
    public class CarsController : Controller
    {
        private readonly ICarService _cars;

        public CarsController(ICarService cars)
        {
            this._cars = cars;
        }

        //[Route("cars/{make}")]
        //public IActionResult ByMake(string make)
        //{
        //    var cars = this._cars.ByMake(make);

        //    return View(new CarsByMakeModel
        //    {
        //        Make = make,
        //        Cars = cars
        //    });
        //}
        
        [Route("parts", Order = 1)]
        public IActionResult Parts()
            => 
            View(this._cars.WithParts());

            


    }
}
