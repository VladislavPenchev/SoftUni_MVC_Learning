namespace CarDealer.Services.Models.Sales
{
    using CarDealer.Services.Cars.Models;

    public class SaleDetailsModel : SaleListModel
    {
        public CarModel Car { get; set; }
    }
}
