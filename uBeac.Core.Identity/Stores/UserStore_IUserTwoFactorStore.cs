using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace uBeac.Core.Identity
{
    public partial class UserStore<TUser, TRole, TKey> : IUserTwoFactorStore<TUser>
    {
        public async Task<bool> GetTwoFactorEnabledAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var foundUser = await _repository.GetById(user.Id, cancellationToken);

            return foundUser?.TwoFactorEnabled ?? user.TwoFactorEnabled;
        }

        public async Task SetTwoFactorEnabledAsync(TUser user, bool enabled, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            user.TwoFactorEnabled = enabled;

            var dbUser = await _repository.GetById(user.Id, cancellationToken);
            dbUser.TwoFactorEnabled = enabled;

            await _repository.Replace(dbUser, cancellationToken);
        }
    }
}
