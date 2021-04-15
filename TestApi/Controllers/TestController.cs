using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using uBeac.Core.Web;

namespace TestApi.Controllers
{
    public class TestController: BaseController
    {
        private readonly IConfigurationRoot _config;
        public TestController(IConfigurationRoot config)
        {
            _config = config;
        }

        [HttpGet]
        public object Get() 
        {
            return 1;
        }

    }
}
