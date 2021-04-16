using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using uBeac.Core.Common;
using uBeac.Core.Repositories.Abstractions;

namespace uBeac.Core.Repositories.MongoDB
{
    public class MongoBaseEntityRepository<TKey, TEntity> : MongoEntityRepository<TKey, TEntity>, IBaseEntityRepository<TKey, TEntity>
         where TEntity : IBaseEntity<TKey>
         where TKey : IEquatable<TKey>
    {
        public MongoBaseEntityRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
        {
        }
        public override Task Insert(TEntity entity, CancellationToken cancellationToken = default)
        {
            var now = DateTime.Now;
            entity.CreateDate = now;
            entity.UpdateDate = now;
            return base.Insert(entity, cancellationToken);
        }

        public override Task InsertMany(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            var now = DateTime.Now;
            foreach (var entity in entities)
            {
                entity.CreateDate = now;
                entity.UpdateDate = now;
            }
            return base.InsertMany(entities, cancellationToken);
        }

        public override Task<TEntity> Replace(TEntity entity, CancellationToken cancellationToken = default)
        {
            entity.UpdateDate = DateTime.Now;
            return base.Replace(entity, cancellationToken);
        }
    }

    public class MongoBaseEntityRepository<TEntity> : MongoBaseEntityRepository<Guid, TEntity>, IBaseEntityRepository<TEntity>
        where TEntity : IBaseEntity
    {
        public MongoBaseEntityRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
        {
        }

        public override Task Insert(TEntity entity, CancellationToken cancellationToken = default)
        {
            entity.Id = Guid.NewGuid();
            return base.Insert(entity, cancellationToken);
        }

        public override Task InsertMany(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entities)
                entity.Id = Guid.NewGuid();

            return base.InsertMany(entities, cancellationToken);
        }
    }

}
