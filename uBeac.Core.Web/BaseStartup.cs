using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace uBeac.Core.Web
{
    public class BaseStartup
    {
        public IConfigurationRoot Configuration { get; }
        public IHostEnvironment Environment { get; }

        public BaseStartup(IHostEnvironment env)
        {
            Environment = env;
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonConfig(env);
            //configBuilder.AddEnvironmentVariables();
            Configuration = configBuilder.Build();
        }

        public virtual void ConfigureServices(IServiceCollection services) 
        {
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton(Configuration);
        }

    }
}
