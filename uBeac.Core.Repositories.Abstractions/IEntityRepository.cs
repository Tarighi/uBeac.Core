using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using uBeac.Core.Common;

namespace uBeac.Core.Repositories.Abstractions
{
    public interface IEntityRepository<TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : IEntity<TKey>
    {
        Task Insert(TEntity entity, CancellationToken cancellationToken = default);
        Task InsertMany(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task<TEntity> Replace(TEntity entity, CancellationToken cancellationToken = default);
        Task<bool> Delete(TKey id, CancellationToken cancellationToken = default);
        Task<long> DeleteMany(IEnumerable<TKey> ids, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken = default);
        Task<TEntity> GetById(TKey id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetByIds(IEnumerable<TKey> ids, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);
    }

    public interface IEntityRepository<TEntity> : IEntityRepository<Guid, TEntity>
        where TEntity : IEntity
    {
    }
}
