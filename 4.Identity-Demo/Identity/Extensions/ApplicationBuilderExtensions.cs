namespace Identity.Extensions
{
    using Identity.Data;
    using Identity.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Threading.Tasks;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<ApplicationDbContext>().Database.Migrate();

                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                Task
                    .Run(async () =>
                    {
                        var roleAdmin = GlobalConstants.AdministratorRole;

                        var roleExists = await roleManager.RoleExistsAsync(roleAdmin);

                        if (!roleExists)
                        {
                            await roleManager.CreateAsync(new IdentityRole
                            {
                                Name = roleAdmin
                            });
                        }

                        var adminUser = await userManager.FindByNameAsync(roleAdmin);

                        if (adminUser == null)
                        {
                            adminUser = new User
                            {
                                Email = "admin@mysite.com",
                                UserName = "admin@mysite.com",
                            };

                            await userManager.CreateAsync(adminUser, "admin12");

                            await userManager.AddToRoleAsync(adminUser,roleAdmin);
                        }
                    })
                    .Wait();

            }

            return app;
        }
    }
}
