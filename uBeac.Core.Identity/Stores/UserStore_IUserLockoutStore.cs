using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace uBeac.Core.Identity
{
    public partial class UserStore<TUser, TRole, TKey> : IUserLockoutStore<TUser>
    {
        public async Task<int> GetAccessFailedCountAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return (await _repository.GetById(user.Id, cancellationToken))?.AccessFailedCount ?? user.AccessFailedCount;
        }

        public async Task<bool> GetLockoutEnabledAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return (await _repository.GetById(user.Id, cancellationToken))?.LockoutEnabled ?? user.LockoutEnabled;
        }

        public async Task<int> IncrementAccessFailedCountAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            user.AccessFailedCount++;

            var dbUser = await _repository.GetById(user.Id, cancellationToken);
            dbUser.AccessFailedCount = user.AccessFailedCount;

            await _repository.Replace(dbUser, cancellationToken);

            return user.AccessFailedCount;
        }

        public async Task ResetAccessFailedCountAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            user.AccessFailedCount = 0;

            var dbUser = await _repository.GetById(user.Id, cancellationToken);
            dbUser.AccessFailedCount = user.AccessFailedCount;

            await _repository.Replace(dbUser, cancellationToken);
        }

        public async Task<DateTimeOffset?> GetLockoutEndDateAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return (await _repository.GetById(user.Id, cancellationToken))?.LockoutEnd ?? user.LockoutEnd;
        }

        public async Task SetLockoutEndDateAsync(TUser user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            user.LockoutEnd = lockoutEnd;

            var dbUser = await _repository.GetById(user.Id, cancellationToken);
            dbUser.LockoutEnd = lockoutEnd;

            await _repository.Replace(dbUser, cancellationToken);
        }

        public async Task SetLockoutEnabledAsync(TUser user, bool enabled, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            user.LockoutEnabled = enabled;

            var dbUser = await _repository.GetById(user.Id, cancellationToken);
            dbUser.LockoutEnabled = enabled;

            await _repository.Replace(dbUser, cancellationToken);
        }
    }
}
