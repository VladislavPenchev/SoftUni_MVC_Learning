namespace CarDealer.Services.Models.Customers
{
    using CarDealer.Services.Models.Sales;
    using System.Collections.Generic;
    using System.Linq;

    public class CustomerTotalSalesModel : CustomerModel
    {                
        public IEnumerable<SaleModel> BoughtCars { get; set; }

        public int TotalBoughtCars => this.BoughtCars.Count();

        public decimal TotalMoneySpent
        {
            get
                => this.BoughtCars.Sum(c => c.Price * (1 - (decimal)c.Discout))
                    * (this.IsYoungDriver ? 0.95m : 1);
        }
    }
}
