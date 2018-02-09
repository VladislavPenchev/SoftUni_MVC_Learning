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
        public IActionResult Create(CustomerFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this._customer.Create(
                model.Name,
                model.BirthDay,
                model.IsYoungDriver);

            return RedirectToAction(nameof(All), new { order = OrderDirection.Ascending.ToString() });
        }

        [Route(nameof(Edit) + "/{id}")]
        public IActionResult Edit(int id)
        {
            var customer = this._customer.ById(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(new CustomerFormModel
            {
                Name = customer.Name,
                BirthDay = customer.BirthDay,
                IsYoungDriver = customer.IsYoungDriver
            });
        }

        [Route(nameof(Edit) + "/{id}")]
        [HttpPost]
        public IActionResult Edit(int id, CustomerFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var customerExists = this._customer.Exists(id);

            if (!customerExists)
            {
                return NotFound();
            }

            this._customer.Edit(
                id,
                model.Name,
                model.BirthDay,
                model.IsYoungDriver
                );

            return RedirectToAction(nameof(All), new { order = OrderDirection.Ascending.ToString() });
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
