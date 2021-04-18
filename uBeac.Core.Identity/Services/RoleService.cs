using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace uBeac.Core.Identity
{
    public interface IRoleService<TKey, TRole>
        where TKey : IEquatable<TKey>
        where TRole : Role<TKey>
    {
        Task Insert(TRole role, CancellationToken cancellationToken = default);
        Task<bool> Delete(TKey id, CancellationToken cancellationToken = default);
        Task<bool> Update(TRole role, CancellationToken cancellationToken = default);
        Task<IEnumerable<TRole>> GetAll(CancellationToken cancellationToken = default);
    }

    public interface IRoleService<TRole> : IRoleService<Guid, TRole>
        where TRole : Role
    {
    }

    public class RoleService<TKey, TRole> : IRoleService<TKey, TRole>
        where TKey : IEquatable<TKey>
        where TRole : Role<TKey>
    {

        private readonly RoleStore<TRole, TKey> _roleStore;

        public RoleService(RoleStore<TRole, TKey> roleStore)
        {
            _roleStore = roleStore;
        }

        public async Task<bool> Delete(TKey id, CancellationToken cancellationToken = default)
        {
            var role = await _roleStore.FindByIdAsync(id.ToString(), cancellationToken);

            if (role is null)
                throw new Exception("Role Id does not exist!");

            var idResult = await _roleStore.DeleteAsync(role, cancellationToken);

            if (!idResult.Succeeded)
            {
                var message = string.Empty;
                throw new Exception(string.Join("\r\n", idResult.Errors.Select(x => x.Code + "," + x.Description)));
            }

            return true;
        }

        public Task<IEnumerable<TRole>> GetAll(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_roleStore.Roles.AsEnumerable());
        }

        public async Task Insert(TRole role, CancellationToken cancellationToken = default)
        {
            var idResult =  await _roleStore.CreateAsync(role, cancellationToken);

            if (!idResult.Succeeded)
            {
                var message = string.Empty;
                throw new Exception(string.Join("\r\n", idResult.Errors.Select(x => x.Code + "," + x.Description)));
            }
        }

        public async Task<bool> Update(TRole role, CancellationToken cancellationToken = default)
        {
            var idResult= await _roleStore.UpdateAsync(role, cancellationToken);

            if (!idResult.Succeeded)
            {
                var message = string.Empty;
                throw new Exception(string.Join("\r\n", idResult.Errors.Select(x => x.Code + "," + x.Description)));
            }
            return true;
        }
    }

    public class RoleService<TRole> : RoleService<Guid, TRole>, IRoleService<TRole>
        where TRole : Role
    {
        public RoleService(RoleStore<TRole> roleStore) : base(roleStore)
        {
        }
    }
}
