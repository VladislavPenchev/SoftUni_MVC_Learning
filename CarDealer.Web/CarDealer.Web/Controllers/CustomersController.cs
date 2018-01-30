namespace CarDealer.Web.Controllers
{
    using Services;
    using Models.Customers;
    using Microsoft.AspNetCore.Mvc;
    using Services.Models;

    public class CustomersController : Controller
    {
        private readonly ICustomerService _customer;

        public CustomersController(ICustomerService customers)
        {
            this._customer = customers;
        }
        [Route("customers/all/{order}")]
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
    }
}
