using System;
using uBeac.Core.Repositories.MongoDB;

namespace uBeac.Core.Identity.MongoDB
{
    public class MongoRoleRepository<TKey, TRole> : MongoEntityRepository<TKey, TRole>, IRoleRepository<TKey, TRole>
       where TKey : IEquatable<TKey>
       where TRole : Role<TKey>
    {
        public MongoRoleRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
        {
        }
    }

    public class MongoRoleRepository<TRole> : MongoEntityRepository<TRole>, IRoleRepository<TRole>
        where TRole : Role
    {
        public MongoRoleRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
        {
        }
    }
}
