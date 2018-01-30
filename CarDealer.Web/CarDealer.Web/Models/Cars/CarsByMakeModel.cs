﻿namespace CarDealer.Web.Models.Cars
{
    using Services.Models;
    using System.Collections.Generic;

    public class CarsByMakeModel
    {
        public string Make { get; set; }

        public IEnumerable<CarModel> Cars { get; set; }
    }
}
