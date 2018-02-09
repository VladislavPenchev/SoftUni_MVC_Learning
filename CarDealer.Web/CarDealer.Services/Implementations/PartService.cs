namespace CarDealer.Services.Implementations
{
    using System.Collections.Generic;
    using CarDealer.Data;
    using CarDealer.Services.Models.Parts;
    using System.Linq;

    public class PartService : IPartService
    {        
        private readonly CarDealerDbContext _db;

        public PartService(CarDealerDbContext db)
        {
            this._db = db;
        }

        public IEnumerable<PartListingModel> All(int page = 1, int pageSize = 10)
            => this
                ._db
                .Parts
                .OrderByDescending(p=>p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new PartListingModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    SupplierName = p.Supplier.Name
                })
                .ToList();

        public int Total() => this._db.Parts.Count();
    }
}
