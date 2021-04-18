using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace uBeac.Core.Identity
{
    public partial class UserStore<TUser, TRole, TKey> : IUserRoleStore<TUser>
    {
        public async Task AddToRoleAsync(TUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var role = await _roleStore.FindByNameAsync(roleName, cancellationToken);
            if (role == null) return;

            user.Roles.Add(role.Id);
        }

        public async Task RemoveFromRoleAsync(TUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var role = await _roleStore.FindByNameAsync(roleName, cancellationToken).ConfigureAwait(true);

            if (role == null) return;

            user.Roles.Remove(role.Id);
        }

        public async Task<IList<TUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var role = await _roleStore.FindByNameAsync(roleName, cancellationToken).ConfigureAwait(true);

            if (role == null) return new List<TUser>();

            return (await _repository.Find(x => x.Roles.Any(r => role.Id.Equals(r)), cancellationToken)).ToList();
        }

        public Task<IList<string>> GetRolesAsync(TUser user, CancellationToken cancellationToken)
        {
            IList<string> roleIds = user.Roles.Select(x => x.ToString()).ToList();
            return Task.FromResult(roleIds);
        }

        public async Task<bool> IsInRoleAsync(TUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var role = await _roleStore.FindByNameAsync(roleName, cancellationToken);
            
            if (role == null) return false;

            return user.Roles.Contains(role.Id);
        }
    }
}
