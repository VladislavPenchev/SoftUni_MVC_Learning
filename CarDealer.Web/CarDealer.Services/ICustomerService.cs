﻿namespace CarDealer.Services
{
    using CarDealer.Services.Models.Customers;
    using Models;
    using System;
    using System.Collections.Generic;

    public interface ICustomerService
    {
        IEnumerable<CustomerModel> Ordered(OrderDirection order);

        CustomerTotalSalesModel TotalSaleById(int id);

        void Create(string name, DateTime birthday,bool isYoungDriver);

    }
}
