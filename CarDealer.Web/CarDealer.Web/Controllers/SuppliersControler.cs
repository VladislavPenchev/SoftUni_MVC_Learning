namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using Microsoft.AspNetCore.Mvc;
    using Models.Suppliers;

    public class SuppliersControler : Controller
    {
        private const string SuppliersView = "Suppliers";

        private readonly ISupplierService supplier;

        public SuppliersControler(ISupplierService supplier)
        {
            this.supplier = supplier;
        }
        [Route("suppliers/local")]
        public IActionResult Local()
            => View("Suppliers", this.GetSupplierModel(false));
        
        public IActionResult Importers()
            => View(SuppliersView, this.GetSupplierModel(true));

        private SuppliersModel GetSupplierModel(bool importers)
        {
            var type = importers ? "Importer" : "Local";

            var suppliers = this.supplier.All(importers);

            return new SuppliersModel
            {
                Type = type,
                Suppliers = suppliers
            };
        }

    }
}
