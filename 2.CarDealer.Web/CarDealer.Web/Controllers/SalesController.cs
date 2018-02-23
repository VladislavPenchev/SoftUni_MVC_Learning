namespace CarDealer.Web.Controllers
{
    using CarDealer.Data;
    using CarDealer.Services;
    using CarDealer.Web.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;

    [Route("Sales")]
    public class SalesController : Controller
    {
        private readonly ISaleService _sales;

        public SalesController(ISaleService sales)
        {
            this._sales = sales;
        }

        [Route("")]
        public IActionResult All()

            => this.View(this._sales.All());

        [Route("{id}")]
        public IActionResult Details(int id)
            => this.ViewOrNotFound(this._sales.Details(id));
        
    }
}
