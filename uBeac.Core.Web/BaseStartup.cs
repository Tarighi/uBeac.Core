using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

        public virtual void ConfigureServices(IServiceCollection services) 
        {
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton(Configuration);
        }

    }
}
