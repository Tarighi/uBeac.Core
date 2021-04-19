using System;

namespace uBeac.Core.Repositories.Abstractions
{
    public interface IBaseEntityRepository<TKey, TEntity> : IEntityRepository<TKey, TEntity>
     where TEntity : IBaseEntity<TKey>
     where TKey : IEquatable<TKey>
    {
    }

    public interface IBaseEntityRepository<TEntity> : IBaseEntityRepository<Guid, TEntity>
       where TEntity : IBaseEntity
    {
    }
}
