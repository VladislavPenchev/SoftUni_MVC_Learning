namespace Identity.Controllers
{
    using Identity.Data;
    using Identity.Extensions;
    using Identity.Models;
    using Identity.Models.Identity;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading.Tasks;

    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class IdentityController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly UserManager<User> _usermanager;

        public IdentityController(ApplicationDbContext db, UserManager<User> usermanager)
        {
            this._db = db;
            this._usermanager = usermanager;
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

        public async Task<IActionResult> Roles(string id)
        {
            var user =  await this._usermanager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var roles = await this._usermanager.GetRolesAsync(user);

            return View(new UserWithRolesViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Roles = roles
            });
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await this._usermanager.CreateAsync(new User
            {
                Email = model.Email,
                UserName = model.Email
            }, model.Password);

            return RedirectToAction(nameof(All));
        }
    }
}
