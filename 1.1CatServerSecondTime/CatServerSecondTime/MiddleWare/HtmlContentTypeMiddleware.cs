namespace CatServerSecondTime.MiddleWare
{
    using CatServerSecondTime.Data;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Threading.Tasks;

    public class HtmlContentTypeMiddleware
    {
        private readonly RequestDelegate next;

        public HtmlContentTypeMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public Task Invoke(HttpContext context)
        {

            return this.next(context);
        }
    }
}
