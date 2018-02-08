namespace CarDealer.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using CarDealer.Data;
    using CarDealer.Data.Models;
    using CarDealer.Services.Models.Sales;

    public class SaleService : ISaleService
    {
        private CarDealerDbContext _db;

        public SaleService(CarDealerDbContext db)
        {
            this._db = db;
        }

        public IEnumerable<SaleListModel> All()

            => this
                ._db
                .Sales
                .Select(s => new SaleListModel
                {
                    CustomerName = s.Customer.Name,
                    Discout = s.Discount,
                    IsYoungDriver = s.Customer.IsYoungDriver,
                    Price = s.Car.Parts.Sum(p => p.Part.Price)
                })
                .ToList();
    }
}
