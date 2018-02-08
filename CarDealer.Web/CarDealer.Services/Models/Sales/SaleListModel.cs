namespace CarDealer.Services.Models.Sales
{
    public class SaleListModel : SaleModel
    {
        public string CustomerName { get; set; }

        public bool IsYoungDriver { get; set; }

        public decimal DiscounterPrice => this.Price * (1 - ((decimal)this.Discout + (this.IsYoungDriver ? 0.05m : 0)));
    }
}
