namespace CatsServer
{
    using Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;
    using System.Net.Http.Headers;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CatsDbContext>(options =>
            options.UseSqlServer("Server=.;Database=CatsServerDb;Integrated Security=True;"));
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Use((context, next) =>
            {
                context.RequestServices.GetRequiredService<CatsDbContext>().Database.Migrate();
                return next();
            });

            app.Use((context, next) =>
            {
                context.Response.Headers.Add("Content-Type", "text/html");
                return next();
            });

            app.MapWhen(
                ctx => ctx.Request.Path.Value == "/"
                && ctx.Request.Method == "GET", 
                home =>
                {
                    home.Run(async (context) =>
                    {
                        await context.Response.WriteAsync($"<h1>{env.ApplicationName}</h1>");

                        var db = context.RequestServices.GetService<CatsDbContext>();

                        var catLinks = db
                            .Cats
                            .Select(c => new
                            {
                                c.Id,
                                c.Name
                            })
                            .ToList();

                        await context.Response.WriteAsync("<ul>");
                        await context.Response.WriteAsync(@"
                              <form action =""/cat/add"">
                              <input type =""submit"" value=""Add Cat""/>        
                              </form>");

                        foreach (var cat in catLinks)
                        {
                            await context.Response.WriteAsync($@"<li><a href=""/cat/{cat.Id}>{cat.Name}</a></li>");
                        }

                    });
                });

            app.Run(async (context) =>
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("404 Page Was Not Found :/");
            });
        }
    }
}
