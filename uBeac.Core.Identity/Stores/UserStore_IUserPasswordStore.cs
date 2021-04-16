using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace uBeac.Core.Identity
{
    public partial class UserStore<TUser, TRole, TKey> : IUserPasswordStore<TUser>
    {
        public Task<string> GetPasswordHashAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.PasswordHash);
        }

        public async Task<bool> HasPasswordAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return (await _repository.GetById(user.Id, cancellationToken))?.PasswordHash != null;
        }

        public async Task SetPasswordHashAsync(TUser user, string passwordHash, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            user.PasswordHash = passwordHash;

            var dbUser = await _repository.GetById(user.Id, cancellationToken);
            dbUser.PasswordHash = passwordHash;

            await _repository.Replace(user, cancellationToken);
        }
    }
}
