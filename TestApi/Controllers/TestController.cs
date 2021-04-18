using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestApi.Controllers
{
    [Route("api/[controller]/[action]/")]
    [ApiController]
    public class TestController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TestController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public string TestA()
        {
            return _httpContextAccessor.HttpContext.User.Identity.Name;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public string TestB()
        {
            return _httpContextAccessor.HttpContext.User.Identity.Name;
        }
    }
}
