using System;
using uBeac.Core.Repositories.MongoDB;

namespace uBeac.Core.Identity.MongoDB
{
    public class MongoUserRepository<TKey, TUser> : MongoEntityRepository<TKey, TUser>, IUserRepository<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser : User<TKey>
    {
        public MongoUserRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
        {
        }
    }

    public class MongoUserRepository<TUser> : MongoEntityRepository<TUser>, IUserRepository<TUser>
        where TUser : User
    {
        public MongoUserRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
        {
        }
    }
}
