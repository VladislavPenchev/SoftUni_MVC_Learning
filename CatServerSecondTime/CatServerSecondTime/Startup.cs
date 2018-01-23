namespace CatServerSecondTime
{
    using Infrastructure;
    using Data;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;
    using System;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CatsDbContext>(options => options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=CatsServerDb;Integrated Security=True;"));

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDataBaseMigraiton();
            app.UseStaticFiles();
            app.UseHtmlContentType();

            app.MapWhen(
                ctx => ctx.Request.Path.Value == "/"
                && ctx.Request.Method == HttpMethod.Get,
                home =>
            {
                home.Run(async (context) =>
                {
                    await context.Response.WriteAsync($"<h1>{env.ApplicationName}</h1>");

                    var db = context.RequestServices.GetRequiredService<CatsDbContext>();

                    using (db)
                    {
                        var catData = db
                            .Cats
                            .Select(c => new
                            {
                                c.Id,
                                c.Name
                            })
                            .ToList();
                        await context.Response.WriteAsync("<ul>");

                        foreach (var cat in catData)
                        {
                            await context.Response.WriteAsync($@"<li><a href=""/cats/{cat.Id}"">{cat.Name}</a></li>");
                        }

                        await context.Response.WriteAsync("</ul>");
                        await context.Response.WriteAsync(@"
                        <form action=""/cat/add"">
                            <input type=""submit"" value=""Add Cat""/>
                        </form>");
                    }


                });
            });

            app.MapWhen(
                ctx => ctx.Request.Path.Value == "/cat/add",
                    catAdd =>
                    {
                        catAdd.Run(async (context) =>
                        {
                            if (context.Request.Method == HttpMethod.Get)
                            {
                                context.Response.Redirect("/cats-add-form.html");
                            }
                            else if (context.Request.Method == HttpMethod.Post)
                            {
                                
                                var formData = context.Request.Form;
                                
                                var age = 0;
                                int.TryParse(formData["Age"], out age);

                                var cat = new Cat
                                {
                                    Name = formData["Name"],
                                    Age = age,
                                    Bread = formData["Bread"],
                                    ImageUrl = formData["ImageUrl"]
                                };


                                
                                try
                                {
                                    if (string.IsNullOrWhiteSpace(cat.Name)
                                    || string.IsNullOrWhiteSpace(cat.Bread)
                                    || string.IsNullOrWhiteSpace(cat.ImageUrl))
                                    {
                                        throw new InvalidOperationException("Invalid cat data.");
                                    }
                                    var db = context.RequestServices.GetRequiredService<CatsDbContext>();

                                    using (db)
                                    {
                                        db.Add(cat);

                                        await db.SaveChangesAsync();
                                    }
                                    context.Response.Redirect("/");
                                }
                                catch
                                {
                                    await context.Response.WriteAsync("<h2>Invalid cat data!</h2>");
                                    await context.Response.WriteAsync(@"<a href=""/cat/add"">Back To The Form</a>");
                                }
                            }
                        });
                    });

            app.MapWhen(
                ctx => ctx.Request.Path.Value.StartsWith("/cat")
                    && ctx.Request.Method == HttpMethod.Get,
                catDetails =>
            {
                catDetails.Run(async (context) =>
                {
                    var urlParts = context
                        .Request
                        .Path
                        .Value
                        .Split('/',StringSplitOptions.RemoveEmptyEntries);

                    if (urlParts.Length < 2)
                    {
                        context.Response.Redirect("/");
                        return;
                    }
                    else
                    {
                        var catId = 0;
                        int.TryParse(urlParts[1], out catId);
                        if (catId == 0)
                        {
                            context.Response.Redirect("/");
                            return;
                        }

                        var db = context.RequestServices.GetRequiredService<CatsDbContext>();

                        using (db)
                        {
                            var cat = await db.Cats.FindAsync(catId);

                            if(cat == null)
                            {
                                context.Response.Redirect("/");
                                return;
                            }

                            await context.Response.WriteAsync($"<h1>{cat.Name}</h1>");
                            await context.Response.WriteAsync($@"<img src=""{cat.ImageUrl}"" alt=""{cat.Name}"" width=""300""/>");
                            await context.Response.WriteAsync($@"<p>Age: {cat.Age}</p>");
                            await context.Response.WriteAsync($@"<p>Breed: {cat.Bread}</p>");
                        }
                    }

                    
                });
            });

            //posledno se execute-va
            app.Run(async (context) =>
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("404 Page Was Not Found :/");

            });
        }
    }
}
