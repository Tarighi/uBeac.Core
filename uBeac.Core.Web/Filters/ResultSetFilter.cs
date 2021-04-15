using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace uBeac.Core.Web.Filters
{
    // todo: should we add to all controllers
    // todo: is timing correct?
    public class ResultSetFilter : IActionFilter
    {
        private Stopwatch stopwatch;
        public void OnActionExecuting(ActionExecutingContext context)
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            stopwatch.Stop();

            if (context.Result != null && typeof(ObjectResult) == context.Result.GetType())
            {
                var objectResult = (ObjectResult)context.Result;
                if (typeof(IResultSet).IsAssignableFrom(objectResult.Value.GetType()))
                {
                    var result = (IResultSet)objectResult.Value;
                    result.Duration = (int)stopwatch.Elapsed.TotalMilliseconds;
                    result.TraceId = context.HttpContext.TraceIdentifier;
                    context.HttpContext.Response.StatusCode = result.Code;
                }
            }
        }
    }
}
