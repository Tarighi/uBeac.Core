using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestApi.Repositories;
using TestApi.Services;
using uBeac.Core.Repositories.MongoDB;
using uBeac.Core.Web;
using uBeac.Core.Web.Middlewares;

namespace TestApi
{
    public class Startup : BaseStartup
    {
        public Startup(IWebHostEnvironment env) : base(env)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUnitService, UnitService>();
            services.AddScoped<IUnitRepository, UnitRepository>();

            services.AddAutoMapper(typeof(MappingProfileForDTOs));

            services.AddMongo<MainDBContext>("TestConnection");

            services.AddSingleton<IConfiguration>(Configuration);
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
