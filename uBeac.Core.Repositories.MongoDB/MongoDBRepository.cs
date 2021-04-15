using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using uBeac.Core.Common;
using uBeac.Core.Repositories.Abstractions;

namespace uBeac.Core.Repositories.MongoDB
{
    public class MongoDBRepository<TKey, TEntity> : IEntityRepository<TKey, TEntity>
         where TKey : IEquatable<TKey>
         where TEntity : IEntity<TKey>
    {
        protected readonly IMongoCollection<TEntity> Collection;
        protected readonly IMongoCollection<BsonDocument> BsonCollection;
        protected readonly IMongoDatabase MongoDatabase;

        public MongoDBRepository(IMongoDatabase mongoDatabase)
        {
            MongoDatabase = mongoDatabase;
            Collection = mongoDatabase.GetCollection<TEntity>(GetCollectionName());
            BsonCollection = mongoDatabase.GetCollection<BsonDocument>(GetCollectionName());
        }

        protected virtual string GetCollectionName()
        {
            return typeof(TEntity).Name;
        }

        protected virtual FilterDefinition<TEntity> GetIdFilter(TKey id)
        {
            return Builders<TEntity>.Filter.Eq("_id", id);
        }

        protected virtual FilterDefinition<TEntity> GetIdsFilter(IEnumerable<TKey> ids)
        {
            return Builders<TEntity>.Filter.In("_id", ids);
        }

        public virtual async Task<bool> Delete(TKey id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var idFilter = GetIdFilter(id);
            var deleteResult = await Collection.DeleteOneAsync(idFilter, cancellationToken);
            return deleteResult.DeletedCount == 1;
        }

        public virtual async Task<long> DeleteMany(IEnumerable<TKey> ids, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var idsFilter = GetIdsFilter(ids);
            var deleteResult = await Collection.DeleteManyAsync(idsFilter, cancellationToken);
            return deleteResult.DeletedCount;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var filter = Builders<TEntity>.Filter.Empty;
            return (await Collection.FindAsync(filter, null, cancellationToken)).ToEnumerable(cancellationToken);
        }

        public virtual async Task<TEntity> GetById(TKey id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var idFilter = GetIdFilter(id);
            return (await Collection.FindAsync(idFilter, null, cancellationToken)).FirstOrDefault(cancellationToken);
        }

        public virtual async Task<IEnumerable<TEntity>> GetByIds(IEnumerable<TKey> ids, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var idsFilter = GetIdsFilter(ids);
            return (await Collection.FindAsync(idsFilter, null, cancellationToken)).ToEnumerable(cancellationToken);
        }

        public virtual async Task Insert(TEntity entity, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await Collection.InsertOneAsync(entity, null, cancellationToken);
        }

        public virtual async Task InsertMany(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await Collection.InsertManyAsync(entities, null, cancellationToken);
        }

        public virtual Task<bool> Update(TEntity entity, CancellationToken cancellationToken = default)
        {
            // todo: implement here
            cancellationToken.ThrowIfCancellationRequested();
            throw new NotImplementedException();
        }

    }
}
