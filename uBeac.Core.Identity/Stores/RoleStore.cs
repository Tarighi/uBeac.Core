using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

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

        public IQueryable<TRole> Roles => _repository.GetAll().Result.AsQueryable(); //todo: implement here

        public RoleStore(IRoleRepository<TKey, TRole> repository)
        {
            _repository = repository;
        }

        public async Task<IdentityResult> CreateAsync(TRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
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
            return Task.FromResult(role.Id.ToString());
        }

        public Task<string> GetRoleNameAsync(TRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        public Task SetRoleNameAsync(TRole role, string roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;
            return Task.FromResult(0);
        }

        public Task<string> GetNormalizedRoleNameAsync(TRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.NormalizedName);
        }

        public Task SetNormalizedRoleNameAsync(TRole role, string normalizedRoleName, CancellationToken cancellationToken)
        {
            role.NormalizedName = normalizedRoleName;
            return Task.FromResult(0);
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
            if (role.Claims is null)
                return await Task.FromResult(new List<Claim>());

            return role.Claims.Select(e => new Claim(e.ClaimType, e.ClaimValue)).ToList();
        }

        public Task AddClaimAsync(TRole role, Claim claim, CancellationToken cancellationToken = default)
        {
            var currentClaim = role.Claims.FirstOrDefault(c => c.ClaimType == claim.Type && c.ClaimValue == claim.Value);

            if (currentClaim == null)
            {
                var identityRoleClaim = new IdentityRoleClaim<TKey>()
                {
                    ClaimType = claim.Type,
                    ClaimValue = claim.Value,
                    RoleId = role.Id,
                    Id = 0 // todo: what should we do?
                };

                role.Claims.Add(identityRoleClaim);
            }

            return Task.FromResult(0);
        }

        public Task RemoveClaimAsync(TRole role, Claim claim, CancellationToken cancellationToken = default)
        {
            role.Claims.RemoveAll(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value);
            return Task.FromResult(0);
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
