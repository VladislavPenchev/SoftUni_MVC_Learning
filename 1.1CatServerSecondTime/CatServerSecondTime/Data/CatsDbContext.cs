namespace CatServerSecondTime.Data
{
    using Microsoft.EntityFrameworkCore;

    public class CatsDbContext :DbContext
    {
        public CatsDbContext(DbContextOptions<CatsDbContext> options)
            :base(options)
        {

        }

        public DbSet<Cat> Cats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
