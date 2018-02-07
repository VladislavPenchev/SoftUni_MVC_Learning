namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using CarDealer.Web.Models.Suppliers;
    using Microsoft.AspNetCore.Mvc;

    public class SuppliersController : Controller
    {
        private readonly ISupplierService _suppliers;

        public SuppliersController(ISupplierService suppliers)
        {
            this._suppliers = suppliers;
        }

        public IActionResult Local()
        {
            return this.View("Suppliers", this.GetSuppliersModel(false));
        }

        public IActionResult Importers()
        {
            return this.View("Suppliers", this.GetSuppliersModel(true));
        }

        private SuppliersModel GetSuppliersModel(bool importers)
        {
            var type = importers ? "Importer" : "Local";

            var suppliers = this._suppliers.All(importers);

            return new SuppliersModel
            {
                Type = type,
                Suppliers = suppliers
            };
        }
    }
}
