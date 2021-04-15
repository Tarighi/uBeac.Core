using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using uBeac.Core.Web;
using uBeac.Core.Web.Middlewares;

namespace TestApi
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IWebHostEnvironment env)
        {
            Environment = env;
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonConfig(env);
            Configuration = configBuilder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);
            services.AddCoreSwaggerWithJWT("TestApi", "v1");

            services.AddHttpContextAccessor();

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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCoreSwagger("doc");
            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
