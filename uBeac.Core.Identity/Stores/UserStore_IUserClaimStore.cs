using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace uBeac.Core.Identity
{
    public partial class UserStore<TUser, TRole, TKey> : IUserClaimStore<TUser>
    {
        public async Task AddClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var dbUser = await _repository.GetById(user.Id, cancellationToken);

            foreach (var claim in claims)
            {
                var identityClaim = new IdentityUserClaim<string>()
                {
                    ClaimType = claim.Type,
                    ClaimValue = claim.Value
                };

                user.Claims.Add(identityClaim);
                dbUser.Claims.Add(identityClaim);
            }

            await _repository.Replace(dbUser, cancellationToken);

        }

        public async Task ReplaceClaimAsync(TUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var dbUser = await _repository.GetById(user.Id, cancellationToken);

            user.Claims.RemoveAll(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value);
            dbUser.Claims.RemoveAll(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value);

            var identityClaim = new IdentityUserClaim<string>()
            {
                ClaimType = newClaim.Type,
                ClaimValue = newClaim.Value
            };

            user.Claims.Add(identityClaim);
            dbUser.Claims.Add(identityClaim);

            await _repository.Replace(dbUser, cancellationToken);
        }

        public async Task RemoveClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var dbUser = await _repository.GetById(user.Id, cancellationToken);

            foreach (var claim in claims)
            {
                user.Claims.RemoveAll(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value);
                dbUser.Claims.RemoveAll(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value);
            }

            await _repository.Replace(dbUser, cancellationToken);
        }

        public async Task<IList<TUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return (await _repository.Find(u => u.Claims.Any(c => c.ClaimType == claim.Type && c.ClaimValue == claim.Value), cancellationToken)).ToList();
        }
    }
}
