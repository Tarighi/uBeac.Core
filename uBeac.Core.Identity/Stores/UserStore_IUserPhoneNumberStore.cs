using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace uBeac.Core.Identity
{
    public partial class UserStore<TUser, TRole, TKey> : IUserPhoneNumberStore<TUser>
    {
        public async Task<string> GetPhoneNumberAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return (await _repository.GetById(user.Id, cancellationToken))?.PhoneNumber;
        }

        public async Task<bool> GetPhoneNumberConfirmedAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return (await _repository.GetById(user.Id, cancellationToken))?.PhoneNumberConfirmed ?? false;
        }

        public async Task SetPhoneNumberAsync(TUser user, string phoneNumber, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            user.PhoneNumber = phoneNumber;

            var dbUser = await _repository.GetById(user.Id, cancellationToken);
            dbUser.PhoneNumber = phoneNumber;

            await _repository.Replace(dbUser, cancellationToken);
        }

        public async Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            user.PhoneNumberConfirmed = confirmed;

            var dbUser = await _repository.GetById(user.Id, cancellationToken);
            dbUser.PhoneNumberConfirmed = confirmed;

            await _repository.Replace(dbUser, cancellationToken);
        }

    }
}
