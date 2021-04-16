using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace uBeac.Core.Identity
{
    public partial class UserStore<TUser, TRole, TKey> : IUserSecurityStampStore<TUser>
    {
        public async Task<string> GetSecurityStampAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return (await _repository.GetById(user.Id, cancellationToken).ConfigureAwait(true))?.SecurityStamp ?? user.SecurityStamp;
        }

        public async Task SetSecurityStampAsync(TUser user, string stamp, CancellationToken cancellationToken)
        {
            user.SecurityStamp = stamp;

            var dbUser = await _repository.GetById(user.Id, cancellationToken);
            dbUser.SecurityStamp = stamp;

            await _repository.Replace(dbUser, cancellationToken);
        }
    }
}
