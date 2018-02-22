namespace Identity.Controllers
{
    using Identity.Data;
    using Identity.Extensions;
    using Identity.Models.Identity;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class IdentityController : Controller
    {
        private ApplicationDbContext _db;

        public IdentityController(ApplicationDbContext db)
        {
            this._db = db;
        }

        public IActionResult All()
        {
            var users = this
                ._db
                .Users
                .OrderBy(u => u.Email)
                .Select(u => new ListUserViewModel
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email
                })
                .ToList();

            return View(users);
        }
    }
}
