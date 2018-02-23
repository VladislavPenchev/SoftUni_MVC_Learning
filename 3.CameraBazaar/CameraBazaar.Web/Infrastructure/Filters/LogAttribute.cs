namespace CameraBazaar.Web.Infrastructure.Filters
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;
    using System.IO;

    public class LogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            using (var writer = new StreamWriter("logs.txt", true))
            {
                var dateTime = DateTime.UtcNow;
                var ipAddress = context.HttpContext.Connection.RemoteIpAddress;
                var userName = context.HttpContext.User?.Identity?.Name ?? "Anonymous";
                var controller = context.Controller.GetType().Name;
                var action = context.RouteData.Values["action"];
                

                var logMessage = $"{dateTime} – {ipAddress} – {userName} – {controller}.{action}";

                if (context.Exception != null)
                {
                    var exceptionType = context.Exception.GetType().Name;
                    var ExceptionMessage = context.Exception.Message;

                    logMessage = $"[!] {logMessage} - {exceptionType} – {ExceptionMessage}";
                }

                writer.WriteLine(logMessage);
            }
        }
    }
}
