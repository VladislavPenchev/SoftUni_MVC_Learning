namespace CarDealer.Web.Infrastructure.Extensions
{
    public static class StringExstensions
    {
        private const string NumberFormat = "F2";

        public static string ToPrice(this double priceText)
        {
            return $"${priceText.ToString("F2")}";
        }

        public static string ToPercentage(this double number)
        {
            return $"{number.ToString(NumberFormat)}%";
        }
    }
}
