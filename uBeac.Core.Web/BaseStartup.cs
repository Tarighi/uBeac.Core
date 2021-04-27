using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using uBeac.Core.Common;

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
            Configuration = configBuilder.Build();
        }

        public virtual void ConfigureServices(IServiceCollection services) 
        {
            services.AddHttpContextAccessor();

            services.AddScoped<IApplicationContext, ApplicationContext>();

            services.AddSingleton<IConfiguration>(Configuration);

            services.AddSingleton(Configuration);

            services.AddMvcCore()
                .AddDataAnnotations()
                .AddApiExplorer()
                .AddFormatterMappings()
                .AddCors();

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
            });
        }

    }
}
