namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using Models.Parts;
    using Microsoft.AspNetCore.Mvc;
    using System;

    public class PartsController : Controller
    {
        private const int pageSize = 2;

        private readonly IPartService _parts;

        public PartsController(IPartService parts)
        {
            this._parts = parts;
        }

        public IActionResult All(int page = 1)
            => View(new PartPageListingModel
            {
                Parts = this._parts.All(page, pageSize),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(this._parts.Total() /(double)pageSize)
            });
    }
}
