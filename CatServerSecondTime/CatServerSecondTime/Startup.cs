namespace CatServerSecondTime
{
    using Infrastructure;
    using Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CatsDbContext>(options => options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=CatsServerDb;Integrated Security=True;"));
            
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    //app.UseDatabaseErrorPage(); // -> ako bazata ne moje da se migrira da pokaje greshka
            //}

            //app.ApplicationServices.GetRequiredService<CatsDbContext>().Database.Migrate();
                       

            app.Use((context, next) =>
            {
                context.RequestServices.GetRequiredService<CatsDbContext>().Database.Migrate();
                return next();
            });

            app.UseStaticFiles();

            app.Use((context,next) =>
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

                    var db = context.RequestServices.GetRequiredService<CatsDbContext>();

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
                });
            });

            app.MapWhen(req => req.Request.Path.Value == "/cat/add",
                    catAdd => {                        
                        catAdd.Run(async (context) =>
                        {
                            if (context.Request.Method == HttpMethod.Get)
                            {
                                context.Response.Redirect("/cats-add-form.html");
                            }
                            else if(context.Request.Method == HttpMethod.Post)
                            {
                                var db = context.RequestServices.GetRequiredService<CatsDbContext>();
                                
                                    var formData = context.Request.Form;

                                    var cat = new Cat
                                    {
                                        Name = formData["Name"],
                                        Age = int.Parse(formData["Age"]),
                                        Bread = formData["Bread"],
                                        ImageUrl = formData["ImageUrl"]
                                    };

                                db.Add(cat);

                                try
                                {
                                    await db.SaveChangesAsync();
                                    
                                    context.Response.Redirect("/");
                                }
                                catch 
                                {
                                    await context.Response.WriteAsync("<h2>Invalid cat data!</h2>");
                                    await context.Response.WriteAsync(@"<a href==""/cat/add"">Back To The Form</a>");
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
