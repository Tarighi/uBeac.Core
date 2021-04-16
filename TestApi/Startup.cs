using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using TestApi.Repositories;
using TestApi.Services;
using uBeac.Core.Identity;
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

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddMongo<MainDBContext>("TestConnection");

            services.AddIdentityMongoDbProvider<MainDBContext, User, Role, Guid>(identity =>
            {
                identity.Password.RequireDigit = false;
                identity.Password.RequireLowercase = false;
                identity.Password.RequireNonAlphanumeric = false;
                identity.Password.RequireUppercase = false;
                identity.Password.RequiredLength = 1;
                identity.Password.RequiredUniqueChars = 0;
            });


            services.AddScoped<IUnitService, UnitService>();
            services.AddScoped<IUnitRepository, UnitRepository>();

            services.AddAutoMapper(typeof(MappingProfileForDTOs));

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

            app.UseAuthentication();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
