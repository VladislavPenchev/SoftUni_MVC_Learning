namespace CarDealer.Services.Implementation
{
    using System.Collections.Generic;
    using CarDealer.Web.Data;
    using System.Linq;
    using CarDealer.Services.Cars.Models;
    using CarDealer.Services.Models.Cars;
    using CarDealer.Services.Models;

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

        public IEnumerable<CarWithPartsModel> WithParts()

            => this
                ._db
                .Cars
                .Select(c => new CarWithPartsModel
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance,
                    Parts = c.Parts.Select( p => new PartModel
                    {
                        Name = p.Part.Name,
                        Price = p.Part.Price
                    })
                })
                .ToList();
    }
}
