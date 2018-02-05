namespace CarDealer.Services.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CarDealer.Web.Data;
    using Models;

    public class CustomerService : ICustomerService
    {
        private readonly CarDealerDbContext db;

        public CustomerService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CustomerModel> OrderedCustomers(OrderDirection order)
        {
            var customersQuery = this.db.Customers.AsQueryable();

            switch (order)
            {
                case OrderDirection.Asending:
                    customersQuery = customersQuery.OrderBy(c => c.BirthDate).ThenBy(c => c.Name);
                    break;
                case OrderDirection.Desending:
                    customersQuery = customersQuery.OrderByDescending(c => c.BirthDate).ThenBy(c => c.Name);
                    break;
                default: throw new InvalidOperationException($"Invalid order direction: {order}");
            }

            return customersQuery
                .Select(c => new CustomerModel
                {
                    Name = c.Name,
                    Birthdate = c.BirthDate,
                    IsYoungDriver = c.isYoungDriver
                })
                .ToList();
        }
    }
}
