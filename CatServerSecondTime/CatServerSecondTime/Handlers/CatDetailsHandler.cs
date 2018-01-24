﻿using System;
using CatServerSecondTime.Data;
using CatServerSecondTime.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CatServerSecondTime.Handlers
{
    public class CatDetailsHandler : IHandler
    {
        public int Order => 3;

        public Func<HttpContext, bool> Condition => ctx => ctx.Request.Path.Value.StartsWith("/cat")
                                                    && ctx.Request.Method == HttpMethod.Get;

        public RequestDelegate RequestHandler => async (context) =>
        {
            var urlParts = context
                .Request
                .Path
                .Value
                .Split('/', StringSplitOptions.RemoveEmptyEntries);

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

                    if (cat == null)
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


        };
    }
}
