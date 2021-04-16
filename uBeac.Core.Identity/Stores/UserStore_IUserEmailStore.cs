using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace uBeac.Core.Identity
{
    public partial class UserStore<TUser, TRole, TKey> : IUserEmailStore<TUser>
    {
        public async Task<string> GetEmailAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return (await _repository.GetById(user.Id, cancellationToken))?.Email ?? user.Email;
        }

        public async Task<bool> GetEmailConfirmedAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return (await _repository.GetById(user.Id, cancellationToken))?.EmailConfirmed ?? user.EmailConfirmed;
        }

        public async Task<TUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            return (await _repository.Find(x => x.NormalizedEmail == normalizedEmail, cancellationToken)).SingleOrDefault();
        }

        public async Task<string> GetNormalizedEmailAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return (await _repository.GetById(user.Id, cancellationToken))?.NormalizedEmail ?? user.NormalizedEmail;
        }

        public async Task SetEmailConfirmedAsync(TUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;

            var dbUser = await _repository.GetById(user.Id, cancellationToken);
            dbUser.EmailConfirmed = confirmed;

            await _repository.Replace(dbUser, cancellationToken);
        }

        public async Task SetNormalizedEmailAsync(TUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            user.NormalizedEmail = normalizedEmail ?? _normalizer.NormalizeEmail(user.Email);

            var dbUser = await _repository.GetById(user.Id, cancellationToken);
            dbUser.NormalizedEmail = user.NormalizedEmail;

            await _repository.Replace(dbUser, cancellationToken);
        }

        public async Task SetEmailAsync(TUser user, string email, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await SetNormalizedEmailAsync(user, _normalizer.NormalizeEmail(user.Email), cancellationToken).ConfigureAwait(false);

            user.Email = email;
            user.NormalizedEmail = _normalizer.NormalizeEmail(user.Email);

            var dbUser = await _repository.GetById(user.Id, cancellationToken);
            dbUser.NormalizedEmail = user.NormalizedEmail;
            dbUser.Email = user.Email;

            await _repository.Replace(dbUser, cancellationToken);
        }
    }
}
