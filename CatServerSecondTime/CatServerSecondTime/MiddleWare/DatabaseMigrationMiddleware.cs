namespace CatServerSecondTime.MiddleWare
{
    using CatServerSecondTime.Data;
    using CatServerSecondTime.Infrastructure;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class DatabaseMigrationMiddleware
    {
        private readonly RequestDelegate next;
        private readonly CatsDbContext db;

        public DatabaseMigrationMiddleware(
            RequestDelegate next,
            CatsDbContext db)
        {
            this.next = next;
            this.db = db;
            
        }

        public Task Invoke(HttpContext context)
        {
            this.db.Database.Migrate();

            return this.next(context);
        }
    }
}
