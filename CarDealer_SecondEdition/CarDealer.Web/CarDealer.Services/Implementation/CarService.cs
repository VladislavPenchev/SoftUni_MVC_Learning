namespace CarDealer.Services.Implementation
{
    using System.Collections.Generic;
    using Models;
    using CarDealer.Web.Data;
    using System.Linq;

    public class CarService : ICarService
    {
        private readonly CarDealerDbContext _db;

        public CarService(CarDealerDbContext db)
        {
            this._db = db;
        }

        public IEnumerable<CarModel> ByMake(string make)
        
            => this
                ._db
                .Cars
                .Where(c => c.Make.ToLower() == make.ToLower())
                .OrderBy(c => c.Make)
                .ThenByDescending(c => c.TravelledDistance)
                .Select(c => new CarModel
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .ToList();

        
    }
}
