using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace uBeac.Core.Web.Filters
{
    public class ModelStateValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ModelState.IsValid)
            {
                await next();
            }
            else
            {
                var result = new ResultSet<string>("Bad request!")
                {
                    Code = StatusCodes.Status400BadRequest
                };

                foreach (string key in context.ModelState.Keys)
                {
                    foreach (var error in context.ModelState[key].Errors)
                    {
                        result.Errors.Add(new Error { Code = key, Description = key + "," + error.ErrorMessage });
                    }
                }
                context.Result = new BadRequestObjectResult(result);

            }
        }
    }
}
