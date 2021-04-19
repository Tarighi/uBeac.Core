using Microsoft.AspNetCore.Identity;
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

        private readonly RoleManager<TRole> _roleManager;

        public RoleService(RoleManager<TRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public virtual async Task<bool> Delete(TKey id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var role = await _roleManager.FindByIdAsync(id.ToString());

            if (role is null)
                throw new Exception("Role Id does not exist!");

            var idResult = await _roleManager.DeleteAsync(role);

            if (!idResult.Succeeded)
            {
                var message = string.Empty;
                throw new Exception(string.Join("\r\n", idResult.Errors.Select(x => x.Code + "," + x.Description)));
            }

            return true;
        }

        public virtual Task<IEnumerable<TRole>> GetAll(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(_roleManager.Roles.AsEnumerable());
        }

        public virtual async Task Insert(TRole role, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var idResult = await _roleManager.CreateAsync(role);

            if (!idResult.Succeeded)
            {
                var message = string.Empty;
                throw new Exception(string.Join("\r\n", idResult.Errors.Select(x => x.Code + "," + x.Description)));
            }
        }

        public virtual async Task<bool> Update(TRole role, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var idResult = await _roleManager.UpdateAsync(role);

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
        public RoleService(RoleManager<TRole> roleManager) : base(roleManager)
        {
        }
    }
}
