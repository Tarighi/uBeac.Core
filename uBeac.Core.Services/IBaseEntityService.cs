using System;

namespace uBeac.Core.Services
{
    public interface IBaseEntityService<TKey, TEntity> : IEntityService<TKey, TEntity>
       where TEntity : IBaseEntity<TKey>
       where TKey : IEquatable<TKey>
    {
    }

    public interface IBaseEntityService<TEntity> : IBaseEntityService<Guid, TEntity>
        where TEntity : IBaseEntity<Guid>
    {
    }
}
