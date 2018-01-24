namespace CatsWebApp.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using CatsWebApp.Models;

    public class CatsDbContext : IdentityDbContext<ApplicationUser>
    {
        public CatsDbContext(DbContextOptions<CatsDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
