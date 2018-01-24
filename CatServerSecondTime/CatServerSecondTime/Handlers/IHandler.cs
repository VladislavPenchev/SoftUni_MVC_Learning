namespace CatServerSecondTime.Handlers
{
    using Microsoft.AspNetCore.Http;
    using System;

    public interface IHandler
    {
        int Order { get; }

        Func<HttpContext, bool> Condition { get;} //ednakvo s lambda expression

        RequestDelegate RequestHandler { get; }
    }
}
