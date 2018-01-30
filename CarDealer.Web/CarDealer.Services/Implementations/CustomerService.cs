namespace CarDealer.Services.Implementations
{
    using System.Collections.Generic;
    using Models;
    using Data;
    using System.Linq;
    using System;

    public class CustomerService : ICustomerService
    {
        private readonly CarDealerDbContext db;

        public CustomerService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CustomerModel> Ordered(OrderDirection order)
        {
            var customersQuery = this.db.Customers.AsQueryable();

            switch (order)
            {
                case OrderDirection.Ascending:
                    customersQuery = customersQuery
                        .OrderBy(c => c.BirthDay)
                        .ThenBy(c => c.Name);
                    break;
                case OrderDirection.Descending:
                    customersQuery = customersQuery
                        .OrderByDescending(c => c.BirthDay)
                        .ThenBy(c => c.Name);
                    break;
                default:
                    throw new InvalidOperationException($"Invalid order direction: {order}.");
            }

            return customersQuery
                        .Select(c => new CustomerModel
                        {
                            Name = c.Name,
                            BirthDay = c.BirthDay,
                            IsYoungDriver = c.IsYoungDriver
                        })
                        .ToList();
        }

    }
}
