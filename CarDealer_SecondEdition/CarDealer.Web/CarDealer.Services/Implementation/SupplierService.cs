namespace CarDealer.Services.Implementation
{
    using System.Collections.Generic;
    using System.Linq;
    using CarDealer.Data.Models;
    using CarDealer.Services.Models;
    using CarDealer.Web.Data;

    public class SupplierService : ISupplierService
    {
        private CarDealerDbContext _db;

        public SupplierService(CarDealerDbContext db)
        {
            this._db = db;
        }

        public IEnumerable<SupplierModel> All(bool isImporter)
        {
            return this
                    ._db
                    .Suppliers
                    .Where(s => s.isImporter == isImporter)
                    .Select(s =>new SupplierModel
                    {
                        Id = s.Id,
                        Name = s.Name,
                        TotalParts = s.Parts.Count()
                    })
                    .ToList();
        }
    }
}
