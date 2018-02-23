namespace CameraBazaar.Web.Infrastructure.Filters
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;
    using System.Diagnostics;
    using System.IO;

    public class MeasureTimeAttribute : ActionFilterAttribute
    {
        private Stopwatch stopWacth;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            this.stopWacth = Stopwatch.StartNew();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            this.stopWacth.Stop();

            using (var writer = new StreamWriter("action-time.txt",true))
            {
                //{date and time} – {Controller}.{Action} – {elapsed time}

                var dateTime = DateTime.UtcNow;
                var controller = context.Controller.GetType().Name;
                var action = context.RouteData.Values["action"];
                var elapsedTime = this.stopWacth.Elapsed;

                var logMessage = $"{dateTime} – {controller}.{action} – {elapsedTime}";

                writer.WriteLine(logMessage);

            }
        }
    }
}
