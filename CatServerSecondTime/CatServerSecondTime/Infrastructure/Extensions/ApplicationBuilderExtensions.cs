using CatServerSecondTime.MiddleWare;
using Microsoft.AspNetCore.Builder;

namespace CatServerSecondTime.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDataBaseMigraiton(
            this IApplicationBuilder builder)        
            => builder.UseMiddleware<DatabaseMigrationMiddleware>();

        public static IApplicationBuilder UseHtmlContentType(
            this IApplicationBuilder builder)
            => builder.UseMiddleware<DatabaseMigrationMiddleware>();
    }
}
