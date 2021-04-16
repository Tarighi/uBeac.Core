using Microsoft.AspNetCore.Mvc;
using uBeac.Core.Web;

namespace TestApi.Controllers
{
    public class TestController : BaseController
    {
        [Get]
        public IResultSet<int> Get()
        {
            var x = 1;
            return x.ToResultSet();
        }

    }
}
