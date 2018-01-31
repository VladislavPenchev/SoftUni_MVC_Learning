using CarDealer.Data;
using CarDealer.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace ImportsData
{
    public class StartUp
    {
        public static void Main()
        {
            DataImport();
        }

        private static void DataImport()
        {
            var jsonSuppliers = File.ReadAllText(@"..\..\Imports\suppliers.json");

            var suppliers = JsonConvert.DeserializeObject<List<Supplier>>(jsonSuppliers);

            var jsonParts = File.ReadAllText(@"..\..\Imports\parts.json");

            var parts = JsonConvert.DeserializeObject<List<Part>>(jsonParts);

            var jsonCars = File.ReadAllText(@"..\..\Imports\cars.json");

            var cars = JsonConvert.DeserializeObject<List<Car>>(jsonCars);

            var jsonCustomers = File.ReadAllText(@"..\..\Imports\customers.json");

            var customers = JsonConvert.DeserializeObject<List<Customer>>(jsonCustomers);

            using (var context = new CarDealerDbContext())
            {
                context.Suppliers.AddRange(suppliers);

                context.SaveChanges();

                suppliers = context.Suppliers.ToList();

                Random rnd = new Random();

                foreach (var part in parts)
                {
                    int randomSupplierIndex = rnd.Next(0, suppliers.Count - 1);

                    part.Supplier = suppliers[randomSupplierIndex];
                }

                context.Parts.AddRange(parts);

                context.SaveChanges();

                parts = context.Parts.ToList();

                foreach (var car in cars)
                {
                    int partsCount = rnd.Next(10, 20);

                    List<int> addedPartsIndexes = new List<int>();

                    while (addedPartsIndexes.Count <= partsCount)
                    {

                        int randomPartIndex = rnd.Next(0, parts.Count - 1);

                        while (addedPartsIndexes.Contains(randomPartIndex))
                        {
                            randomPartIndex = rnd.Next(0, parts.Count - 1);
                        }

                        addedPartsIndexes.Add(randomPartIndex);

                        car.Parts.Add(parts[randomPartIndex]);
                    }
                }

                context.Cars.AddRange(cars);

                context.SaveChanges();

                context.Customers.AddRange(customers);

                context.SaveChanges();

                cars = context.Cars.ToList();
                customers = context.Customers.ToList();

                List<Sale> sales = new List<Sale>();

                for (int i = 0; i < 100; i++)
                {
                    int randomCarIndex = rnd.Next(0, cars.Count - 1);
                    int randomCustomerIndex = rnd.Next(0, customers.Count - 1);
                    int discountMultiplier = rnd.Next(0, 10);

                    while (discountMultiplier == 5 || discountMultiplier == 7 || discountMultiplier == 9)
                    {
                        discountMultiplier = rnd.Next(0, 10);
                    }

                    Sale sale = new Sale
                    {
                        Car = cars[randomCarIndex],
                        Customer = customers[randomCustomerIndex],
                        Discount = (discountMultiplier * 5) / (decimal)100
                    };

                    sales.Add(sale);
                }

                context.Sales.AddRange(sales);

                context.SaveChanges();
            }
        }
    }
}
