namespace CarDealer.Services
{
    using CarDealer.Services.Cars.Models;
    using Models.Cars;
    using System.Collections.Generic;

    public interface ICarService
    {
        IEnumerable<CarModel> ByMake(string make);

        IEnumerable<CarWithPartsModel> WithParts();
    }
}
