using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace uBeac.Core.Identity
{
    public partial class UserStore<TUser, TRole, TKey> : IUserAuthenticatorKeyStore<TUser>
    {
        public async Task<string> GetAuthenticatorKeyAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return (await _repository.GetById(user.Id, cancellationToken))?.AuthenticatorKey ?? user.AuthenticatorKey;
        }

        public async Task SetAuthenticatorKeyAsync(TUser user, string key, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            user.AuthenticatorKey = key;

            var dbUser = await _repository.GetById(user.Id, cancellationToken);
            dbUser.AuthenticatorKey = key;

            await _repository.Replace(dbUser, cancellationToken);
        }
    }
}
