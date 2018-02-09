namespace CarDealer.Web.Controllers
{
    using Services;
    using Models.Customers;
    using Microsoft.AspNetCore.Mvc;
    using Services.Models;
    using CarDealer.Web.Infrastructure.Extensions;

    [Route("customers")]
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customer;

        public CustomersController(ICustomerService customers)
        {
            this._customer = customers;
        }

        [Route(nameof(Create))]
        public IActionResult Create() => View();

        [HttpPost]
        [Route(nameof(Create))]
        public IActionResult Create(CreateCustomerModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this._customer.Create(
                model.Name,
                model.BirthDay,
                model.IsYoungDriver);

            return RedirectToAction(nameof(All), new { order = OrderDirection.Ascending.ToString()});
        }

        [Route(nameof(Edit))]
        public IActionResult Edit(int id)
        {
            return null;
        }

        [Route("all/{order}")]
        public IActionResult All(string order)
        {
            var orderDirection = order.ToLower() == "descending" 
                ? OrderDirection.Descending
                : OrderDirection.Ascending;

            var customers = this._customer.Ordered(orderDirection);

            return View(new AllCustomersModel
            {
                Customers = customers,
                OrderDircetion = orderDirection
            });
        }

        [Route("{id}")]
        public IActionResult TotalSales(int id)
            => this.ViewOrNotFound(this._customer.TotalSaleById(id));
        
    }
}
