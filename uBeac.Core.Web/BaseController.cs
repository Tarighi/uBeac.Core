using Microsoft.AspNetCore.Mvc;
using uBeac.Core.Web.Filters;

namespace uBeac.Core.Web
{
    [Route("api/[controller]/[action]/")]
    [ApiController]
    [Produces("application/json")]
    //[SwaggerResponse(200, "OK")]
    //[SwaggerResponse(400, "Bad Request")]
    //[SwaggerResponse(401, "Unauthorized")]
    //[SwaggerResponse(403, "Forbidden")]
    //[SwaggerResponse(404, "Not Found")]
    //[SwaggerResponse(500, "Unhandled Exception")]

    [TypeFilter(typeof(ModelStateValidationFilter))]
    [TypeFilter(typeof(ResultSetFilter))]
    public abstract class BaseController
    {
        //protected readonly IApplicationContext App;
        //public BaseController(IApplicationContext applicationContext)
        //{
        //    App = applicationContext;
        //}
    }
}
