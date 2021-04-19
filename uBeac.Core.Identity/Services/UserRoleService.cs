using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace uBeac.Core.Identity
{
    public interface IUserRoleService<TKey, TUser, TRole>
        where TKey : IEquatable<TKey>
        where TUser : User<TKey>
        where TRole : Role<TKey>
    {
        Task<bool> AddRoles(TKey userId, IEnumerable<TKey> roleIds, CancellationToken cancellationToken = default);
        Task<bool> RemoveRoles(TKey userId, IEnumerable<TKey> roleIds, CancellationToken cancellationToken = default);
        Task<IEnumerable<TRole>> GetRolesForUser(TKey userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<TUser>> GetUsersInRole(TKey roleId, CancellationToken cancellationToken = default);
    }

    public interface IUserRoleService<TUser, TRole> : IUserRoleService<Guid, TUser, TRole>
        where TUser : User
        where TRole : Role
    {
    }

    public class UserRoleService<TKey, TUser, TRole> : IUserRoleService<TKey, TUser, TRole>
        where TKey : IEquatable<TKey>
        where TUser : User<TKey>
        where TRole : Role<TKey>
    {
        private readonly UserManager<TUser> _userManager;
        private readonly RoleManager<TRole> _roleManager;

        public UserRoleService(UserManager<TUser> userManager, RoleManager<TRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> AddRoles(TKey userId, IEnumerable<TKey> roleIds, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var user = await _userManager.FindByIdAsync(userId.ToString());

            user.ThrowIfNull();

            var idResult = await _userManager.AddToRolesAsync(user, roleIds.Select(x => x.ToString()));

            idResult.ThrowIfInvalid();

            return true;
        }

        public async Task<IEnumerable<TRole>> GetRolesForUser(TKey userId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userManager.FindByIdAsync(userId.ToString());

            user.ThrowIfNull();

            return _roleManager.Roles.Where(x => user.Roles.Contains(x.Id)).AsEnumerable();
        }

        public async Task<IEnumerable<TUser>> GetUsersInRole(TKey roleId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _userManager.GetUsersInRoleAsync(roleId.ToString());
        }

        public async Task<bool> RemoveRoles(TKey userId, IEnumerable<TKey> roleIds, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userManager.FindByIdAsync(userId.ToString());

            user.ThrowIfNull();

            var idResult = await _userManager.RemoveFromRolesAsync(user, roleIds.Select(x => x.ToString()));

            idResult.ThrowIfInvalid();

            return true;
        }
    }

    public class UserRoleService<TUser, TRole> : UserRoleService<Guid, TUser, TRole>, IUserRoleService<TUser, TRole>
        where TUser : User
        where TRole : Role
    {
        public UserRoleService(UserManager<TUser> userManager, RoleManager<TRole> roleManager) : base(userManager, roleManager)
        {
        }
    }

}
