using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel;

namespace uBeac.Core.Identity
{
    public class UserStore<TUser, TRole> : UserStore<TUser, TRole, Guid>
        where TUser : User
        where TRole : Role
    {
        public UserStore(IUserRepository<TUser> repository, IRoleStore<TRole> roleStore) : base(repository, roleStore)
        {
        }
    }

    public partial class UserStore<TUser, TRole, TKey> : IProtectedUserStore<TUser>
      where TKey : IEquatable<TKey>
      where TUser : User<TKey>
      where TRole : Role<TKey>
    {

        private readonly IRoleStore<TRole> _roleStore;
        private readonly IUserRepository<TKey, TUser> _repository;

        public UserStore(IUserRepository<TKey, TUser> repository, IRoleStore<TRole> roleStore)
        {
            _repository = repository;
            _roleStore = roleStore;
        }

        public virtual TKey ConvertIdFromString(string id)
        {
            if (id == null)
                return default;

            return (TKey)TypeDescriptor.GetConverter(typeof(TKey)).ConvertFromInvariantString(id);
        }
    }
}
