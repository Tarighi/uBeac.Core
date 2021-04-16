using Microsoft.AspNetCore.Identity;
using System;
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

            var role = await _roleStore.FindByNameAsync(roleName, cancellationToken).ConfigureAwait(true);
            if (role == null) return;

            user.Roles.Add(roleName);

            var dbUser = await _repository.GetById(user.Id, cancellationToken);
            dbUser.Roles.Add(role.Id.ToString());

            await _repository.Replace(dbUser, cancellationToken);
        }

        public async Task RemoveFromRoleAsync(TUser user, string roleName, CancellationToken cancellationToken)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            cancellationToken.ThrowIfCancellationRequested();

            var role = await _roleStore.FindByNameAsync(roleName, cancellationToken).ConfigureAwait(true);

            if (role == null) return;

            user.Roles.Remove(role.Id.ToString());

            var dbUser = await _repository.GetById(user.Id, cancellationToken);
            dbUser.Roles.Remove(role.Id.ToString());

            await _repository.Replace(dbUser, cancellationToken);
        }

        public async Task<IList<TUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var role = await _roleStore.FindByNameAsync(roleName, cancellationToken).ConfigureAwait(true);

            if (role == null) return new List<TUser>();

            return (await _repository.Find(x => x.Roles.Any(r => role.Id.Equals(r)), cancellationToken)).ToList();
        }

        public async Task<IList<string>> GetRolesAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var userDb = await _repository.GetById(user.Id, cancellationToken);

            if (userDb == null) return new List<string>();

            var roles = new List<string>();

            foreach (var item in userDb.Roles)
            {
                var dbRole = await _roleStore.FindByIdAsync(item, cancellationToken).ConfigureAwait(true);

                if (dbRole != null)
                {
                    roles.Add(dbRole.Name);
                }
            }
            return roles;
        }

        public async Task<bool> IsInRoleAsync(TUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var dbUser = await _repository.GetById(user.Id, cancellationToken);

            var role = await _roleStore.FindByNameAsync(roleName, cancellationToken)
                .ConfigureAwait(true);

            if (role == null) return false;

            return dbUser?.Roles.Contains(role.Id.ToString()) ?? false;
        }

    }
}
