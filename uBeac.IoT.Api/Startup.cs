using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using uBeac.Core.Web;
using uBeac.Core.Identity;
using uBeac.Core.Repositories.MongoDB;
using Microsoft.Extensions.Configuration;
using uBeac.Core.Web.Middlewares;
using uBeac.Core.Services;
using uBeac.Core.Repositories.Abstractions;

namespace uBeac.IoT.Api
{
    public class Startup : BaseStartup
    {
        public Startup(IWebHostEnvironment env) : base(env)
        {
        }
        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddScoped(typeof(IEntityService<>), typeof(EntityService<>));
            services.AddScoped(typeof(IEntityService<,>), typeof(EntityService<,>));
            services.AddScoped(typeof(IBaseEntityService<>), typeof(BaseEntityService<>));
            services.AddScoped(typeof(IBaseEntityService<,>), typeof(BaseEntityService<,>));

            services.AddScoped(typeof(IEntityRepository<>), typeof(MongoEntityRepository<>));
            services.AddScoped(typeof(IEntityRepository<,>), typeof(MongoEntityRepository<,>));
            services.AddScoped(typeof(IBaseEntityRepository<>), typeof(MongoBaseEntityRepository<>));
            services.AddScoped(typeof(IBaseEntityRepository<,>), typeof(MongoBaseEntityRepository<,>));

            services.RegisterServices();

            services.AddJwtAuthentication(Configuration.GetInstance<JwtConfig>("Jwt"));

            services.AddMongo<MainDBContext>("DefaultConnection");

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

            services.AddAutoMapper(typeof(MappingProfileForDTOs));

            services.AddCoreSwaggerWithJWT("uBaec.Api", "v1");
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
            //app.UseMiddleware<AuthenticationMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
