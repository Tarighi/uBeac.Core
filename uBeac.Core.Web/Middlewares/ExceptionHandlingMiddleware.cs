using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace uBeac.Core.Web.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // todo: add more trace/debug information
            var result = new ResultSet
            {
                Code = StatusCodes.Status500InternalServerError,
                TraceId = context.TraceIdentifier
            };

            result.Errors.Add(new Error(exception));
            var serializedResult = JsonConvert.SerializeObject(result);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = result.Code;
            return context.Response.WriteAsync(serializedResult);
        }

    }
}
