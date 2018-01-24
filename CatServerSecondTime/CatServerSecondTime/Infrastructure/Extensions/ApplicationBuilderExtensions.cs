namespace CatServerSecondTime.Infrastructure.Extensions
{
    using CatServerSecondTime.MiddleWare;
    using Microsoft.AspNetCore.Builder;
    using System.Reflection;
    using System.Linq;
    using System;
    using CatServerSecondTime.Handlers;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDataBaseMigraiton(
            this IApplicationBuilder builder)        
            => builder.UseMiddleware<DatabaseMigrationMiddleware>();

        public static IApplicationBuilder UseHtmlContentType(
            this IApplicationBuilder builder)
            => builder.UseMiddleware<DatabaseMigrationMiddleware>();

        public static IApplicationBuilder UseRequestHandlers(
            this IApplicationBuilder builder)
        {
            var handlers = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && typeof(IHandler).IsAssignableFrom(t))
                .Select(Activator.CreateInstance)
                .Cast<IHandler>()
                .OrderBy(h => h.Order);

            foreach (var handler in handlers)
            {
                builder.MapWhen(handler.Condition, app =>
                {
                    app.Run(handler.RequestHandler);
                });
            }

            return builder;
        }
    }
}
