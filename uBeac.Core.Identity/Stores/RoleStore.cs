using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using uBeac.Core.Repositories.Abstractions;

namespace uBeac.Core.Identity
{
    public class RoleStore<TRole> : RoleStore<TRole, Guid>
        where TRole : Role
    {
        public RoleStore(IRoleRepository<TRole> repository) : base(repository)
        {
        }
    }

    public class RoleStore<TRole, TKey> :
        IRoleClaimStore<TRole>,
        IQueryableRoleStore<TRole>
        where TKey : IEquatable<TKey>
        where TRole : Role<TKey>
    {

        private readonly IRoleRepository<TKey, TRole> _repository;

        public IQueryable<TRole> Roles => _repository.GetAll().Result.AsQueryable();

        public RoleStore(IRoleRepository<TKey, TRole> repository)
        {
            _repository = repository;
        }

        public async Task<IdentityResult> CreateAsync(TRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var found = await _repository.Find(x => x.NormalizedName == role.NormalizedName, cancellationToken);

            if (found == null)
                await _repository.Insert(role, cancellationToken);

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(TRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _repository.Replace(role, cancellationToken);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(TRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _repository.Delete(role.Id, cancellationToken);
            return IdentityResult.Success;
        }

        public Task<string> GetRoleIdAsync(TRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(role.Id.ToString());
        }

        public async Task<string> GetRoleNameAsync(TRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return (await _repository.GetById(role.Id, cancellationToken))?.Name ?? role.Name;
        }

        public async Task SetRoleNameAsync(TRole role, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrEmpty(roleName)) throw new ArgumentNullException(nameof(roleName));

            role.Name = roleName;

            await _repository.Replace(role, cancellationToken);
        }

        public Task<string> GetNormalizedRoleNameAsync(TRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(role.NormalizedName);
        }

        public async Task SetNormalizedRoleNameAsync(TRole role, string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            role.NormalizedName = normalizedRoleName;
            await _repository.Replace(role, cancellationToken);
        }

        public async Task<TRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _repository.GetById(ConvertIdFromString(roleId), cancellationToken);
        }

        public async Task<TRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var results = await _repository.Find(x => x.NormalizedName == normalizedRoleName, cancellationToken: cancellationToken);
            return results.SingleOrDefault();
        }

        public async Task<IList<Claim>> GetClaimsAsync(TRole role, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var dbRole = await _repository.GetById(role.Id, cancellationToken);

            return dbRole.Claims.Select(e => new Claim(e.ClaimType, e.ClaimValue)).ToList();
        }

        public async Task AddClaimAsync(TRole role, Claim claim, CancellationToken cancellationToken = default)
        {

            cancellationToken.ThrowIfCancellationRequested();

            var currentClaim = role.Claims.FirstOrDefault(c => c.ClaimType == claim.Type && c.ClaimValue == claim.Value);

            if (currentClaim == null)
            {
                var identityRoleClaim = new IdentityRoleClaim<string>()
                {
                    ClaimType = claim.Type,
                    ClaimValue = claim.Value
                };

                role.Claims.Add(identityRoleClaim);

                await _repository.Replace(role, cancellationToken);
            }

        }

        public Task RemoveClaimAsync(TRole role, Claim claim, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            role.Claims.RemoveAll(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value);

            return _repository.Replace(role, cancellationToken);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual TKey ConvertIdFromString(string id)
        {
            if (id == null)
                return default;

            return (TKey)TypeDescriptor.GetConverter(typeof(TKey)).ConvertFromInvariantString(id);
        }

    }
}
