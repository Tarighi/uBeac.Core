using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace uBeac.Core.Identity
{
    public partial class UserStore<TUser, TRole, TKey> : IQueryableUserStore<TUser>
    {
        public IQueryable<TUser> Users => _repository.GetAll().Result.AsQueryable(); // todo: implement here!
    }
}
