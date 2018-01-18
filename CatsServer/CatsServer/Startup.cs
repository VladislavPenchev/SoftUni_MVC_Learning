namespace CatsServer
{
    using Infrastructure;
    using Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CatsDbContext>(options =>
            options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=CatsServerDb;Initial Catalog=True;"));
        }
        //Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
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
                && ctx.Request.Method == HttpMethod.Get, 
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

            app.MapWhen(req => req.Request.Path.Value == "/cat/add"
                && req.Request.Method == HttpMethod.Get,
                catAdd =>
                {
                    catAdd.Run((context) =>
                    {
                        context.Response.StatusCode = 302;
                        context.Response.Headers.Add("Location", "/cats-add-form.html");
                        return Task.CompletedTask;
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
