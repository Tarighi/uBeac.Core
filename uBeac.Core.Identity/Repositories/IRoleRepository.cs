using System;
using uBeac.Core.Repositories.Abstractions;

namespace uBeac.Core.Identity
{
    public interface IRoleRepository<TKey, TRole> : IEntityRepository<TKey, TRole>
       where TKey : IEquatable<TKey>
       where TRole : Role<TKey>
    {
    }

    public interface IRoleRepository<TRole> : IRoleRepository<Guid, TRole>, IEntityRepository<TRole>
      where TRole : Role
    {
    }
}
