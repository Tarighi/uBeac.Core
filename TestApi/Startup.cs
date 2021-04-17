using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Security.Claims;
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

            services.AddScoped<IApplicationContext, ApplicationContext>();

            services.AddJwtAuthentication(Configuration.GetInstance<JwtConfig>("Jwt"));

            services.AddScoped<IJwtTokenProvider, JwtTokenProvider>();

            services.AddAuthorization();

            services.AddMongo<MainDBContext>("TestConnection");

            services.AddMongoDBIdentity<MainDBContext, User, Role>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
                options.Password.RequiredUniqueChars = 0;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
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
            //app.UseHsts();
            //app.UseHttpsRedirection();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCoreSwagger("doc");
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
