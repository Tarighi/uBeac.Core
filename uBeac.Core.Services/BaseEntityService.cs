using System;
using uBeac.Core.Repositories.Abstractions;

namespace uBeac.Core.Services
{
    public class BaseEntityService<TKey, TEntity> : EntityService<TKey, TEntity>, IBaseEntityService<TKey, TEntity>
      where TEntity : IBaseEntity<TKey>
      where TKey : IEquatable<TKey>
    {
        public BaseEntityService(IBaseEntityRepository<TKey, TEntity> repository):base(repository)
        {
        }
    }

    public class BaseEntityService<TEntity> : BaseEntityService<Guid, TEntity>, IBaseEntityService<TEntity>
        where TEntity : IBaseEntity
    {
        public BaseEntityService(IBaseEntityRepository<TEntity> repository) : base(repository)
        {
        }
    }
}
