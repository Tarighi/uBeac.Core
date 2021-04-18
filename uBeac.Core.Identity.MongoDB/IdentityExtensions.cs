using Microsoft.AspNetCore.Identity;
using System;
using uBeac.Core.Identity;
using uBeac.Core.Identity.MongoDB;
using uBeac.Core.Repositories.MongoDB;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IdentityExtensions
    {
        public static IdentityBuilder AddMongoDBIdentity<TMongoDbContext, TUser, TRole, TKey>(this IServiceCollection services,
           Action<IdentityOptions> setupIdentityAction)
            where TMongoDbContext : class, IMongoDBContext
            where TKey : IEquatable<TKey>
            where TUser : User<TKey>
            where TRole : Role<TKey>
        {

            services.AddScoped(typeof(IUserService<,,>), typeof(UserService<,,>));
            services.AddScoped(typeof(IRoleService<,>), typeof(IRoleService<,>));

            services.AddScoped<IUserRepository<TKey, TUser>>(provider =>
            {
                var dbContext = provider.GetService<TMongoDbContext>();
                return new MongoUserRepository<TKey, TUser>(dbContext);
            });

            services.AddScoped<IRoleRepository<TKey, TRole>>(provider =>
            {
                var dbContext = provider.GetService<TMongoDbContext>();
                return new MongoRoleRepository<TKey, TRole>(dbContext);
            });

            var builder = services.AddIdentity<TUser, TRole>(setupIdentityAction ?? (x => { }));

            builder.AddRoleStore<RoleStore<TRole, TKey>>()
                .AddUserStore<UserStore<TUser, TRole, TKey>>()
                .AddUserManager<UserManager<TUser>>()
                .AddRoleManager<RoleManager<TRole>>()
                .AddDefaultTokenProviders();

            return builder;
        }

        public static IdentityBuilder AddMongoDBIdentity<TMongoDbContext, TUser, TRole>(this IServiceCollection services,
            Action<IdentityOptions> setupIdentityAction)
            where TMongoDbContext : class, IMongoDBContext
            where TUser: User
            where TRole: Role
        {


            services.AddScoped(typeof(IUserService<,>), typeof(UserService<,>));
            services.AddScoped(typeof(IRoleService<>), typeof(RoleService<>));

            services.AddScoped<IUserRepository<TUser>>(provider =>
            {
                var dbContext = provider.GetService<TMongoDbContext>();
                return new MongoUserRepository<TUser>(dbContext);
            });

            services.AddScoped<IRoleRepository<TRole>>(provider =>
            {
                var dbContext = provider.GetService<TMongoDbContext>();
                return new MongoRoleRepository<TRole>(dbContext);
            });

            var builder = services.AddIdentity<TUser, TRole>(setupIdentityAction ?? (x => { }));

            builder.AddRoleStore<RoleStore<TRole>>()
                .AddUserStore<UserStore<TUser, TRole>>()
                .AddUserManager<UserManager<TUser>>()
                .AddRoleManager<RoleManager<TRole>>()
                .AddDefaultTokenProviders();

            return builder;
        }
    }
}
