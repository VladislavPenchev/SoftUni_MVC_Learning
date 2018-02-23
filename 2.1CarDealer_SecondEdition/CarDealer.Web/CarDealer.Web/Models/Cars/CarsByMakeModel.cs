namespace CarDealer.Web.Models.Cars
{
    using System.Collections.Generic;
    using CarDealer.Services.Cars.Models;

    public class CarsByMakeModel
    {
        public string Make { get; set; }

        public IEnumerable<CarModel> Cars { get; set; }
    }
}
