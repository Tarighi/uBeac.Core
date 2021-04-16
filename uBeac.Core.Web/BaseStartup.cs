using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace uBeac.Core.Web
{
    public class BaseStartup
    {
        public IConfigurationRoot Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public BaseStartup(IWebHostEnvironment env)
        {
            Environment = env;
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonConfig(env);
            configBuilder.AddEnvironmentVariables();
            Configuration = configBuilder.Build();
        }
    }
}
