namespace CarDealer.Services.Implementations
{
    using System.Collections.Generic;
    using Models;
    using Data;
    using System.Linq;
    using System;
    using CarDealer.Services.Models.Customers;
    using CarDealer.Services.Models.Sales;
    using CarDealer.Data.Models;

    public class CustomerService : ICustomerService
    {
        private readonly CarDealerDbContext _db;

        public CustomerService(CarDealerDbContext db)
        {
            this._db = db;
        }

        public CustomerModel ById(int id)
            => this
                ._db
                .Customers
                .Where(c => c.Id == id)
                .Select(c => new CustomerModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    BirthDay = c.BirthDate,
                    IsYoungDriver = c.IsYoungDriver
                    
                })
                .FirstOrDefault();

        public void Create(string name, DateTime birthday, bool isYoungDriver)
        {
            var customer = new Customer
            {
                Name = name,
                BirthDate = birthday,
                IsYoungDriver = isYoungDriver
            };

            this._db.Add(customer);
            this._db.SaveChanges();
        }

        public void Edit(int id, string name, DateTime birthDay, bool isYoungDriver)
        {
            var existingCustomer = this._db.Customers.Find(id);

            if (existingCustomer == null)
            {
                return;
            }

            existingCustomer.Name = name;
            existingCustomer.BirthDate = birthDay;
            existingCustomer.IsYoungDriver = isYoungDriver;

            this._db.SaveChanges();
        }

        public bool Exists(int id)
            => this._db.Customers.Any(c => c.Id == id);

        public IEnumerable<CustomerModel> Ordered(OrderDirection order)
        {
            var customersQuery = this._db.Customers.AsQueryable();

            switch (order)
            {
                case OrderDirection.Ascending:
                    customersQuery = customersQuery
                        .OrderBy(c => c.BirthDate)
                        .ThenBy(c => c.Name);
                    break;
                case OrderDirection.Descending:
                    customersQuery = customersQuery
                        .OrderByDescending(c => c.BirthDate)
                        .ThenBy(c => c.Name);
                    break;
                default:
                    throw new InvalidOperationException($"Invalid order direction: {order}.");
            }

            return customersQuery
                        .Select(c => new CustomerModel
                        {   
                            Id = c.Id,
                            Name = c.Name,
                            BirthDay = c.BirthDate,
                            IsYoungDriver = c.IsYoungDriver
                        })
                        .ToList();
        }

        public CustomerTotalSalesModel TotalSaleById(int id)

                 => this
                   ._db
                   .Customers
                   .Where(c => c.Id == id)
                   .Select(c => new CustomerTotalSalesModel
                   {
                       Name = c.Name,
                       IsYoungDriver = c.IsYoungDriver,
                       BoughtCars = c.Sales.Select(s => new SaleModel
                       {
                           Price = s.Car.Parts.Sum(p => p.Part.Price),
                           Discout = s.Discount
                       })
                   })
                   .FirstOrDefault();


    }
}
