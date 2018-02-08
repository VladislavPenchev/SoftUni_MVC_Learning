namespace CarDealer.Web.Controllers
{
    using CarDealer.Data;
    using CarDealer.Services;
    using Microsoft.AspNetCore.Mvc;

    [Route("Sales")]
    public class SalesController : Controller
    {
        private readonly ISaleService _sales;

        public SalesController(ISaleService sales)
        {
            this._sales = sales;
        }

        public IActionResult All()

            => this.View(this._sales.All());

        
    }
}
