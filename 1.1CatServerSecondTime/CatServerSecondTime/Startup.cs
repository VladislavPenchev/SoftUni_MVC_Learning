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
            app.UseRequestHandlers();
                     

            //posledno se execute-va
            app.Run(async (context) =>
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("404 Page Was Not Found :/");

            });
        }
    }
}
