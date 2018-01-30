namespace CarDealer.Services.Implementations
{
    using Data;
    using System.Collections.Generic;
    using CarDealer.Services.Models;
    using System.Linq;

    public class CarService : ICarService
    {
        private readonly CarDealerDbContext _db;

        public CarService(CarDealerDbContext db)
        {
            this._db = db;
        }

        public IEnumerable<CarModel> ByMake(string make)
        {
            return this._db
                .Cars
                .Where(c => c.Make.ToLower() == make.ToLower())
                .OrderBy(c => c.Model)
                .ThenBy(c => c.TravelledDistance)
                .Select(c => new CarModel
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .ToList();
        }
    }
}
