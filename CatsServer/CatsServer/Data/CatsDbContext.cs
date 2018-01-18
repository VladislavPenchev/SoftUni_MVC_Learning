namespace CatsServer.Data
{
    using Microsoft.EntityFrameworkCore;

    public class CatsDbContext : DbContext
    {
        public CatsDbContext(DbContextOptions<CatsDbContext> options)
            : base(options)
        {

        }

        public DbSet<Cat> Cats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=./SQLEXPRESS;Database=CatsServerDb;Trusted_Connection=True;");
        //}
    }
}
