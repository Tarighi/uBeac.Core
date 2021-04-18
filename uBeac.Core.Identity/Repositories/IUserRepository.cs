using System;
using uBeac.Core.Repositories.Abstractions;

namespace uBeac.Core.Identity
{
    public interface IUserRepository<TKey, TUser> : IEntityRepository<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser : User<TKey>
    {
    }

    public interface IUserRepository<TUser> : IUserRepository<Guid, TUser>, IEntityRepository<TUser>
        where TUser : User
    {
    }
}
