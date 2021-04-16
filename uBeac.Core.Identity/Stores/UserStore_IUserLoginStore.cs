using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace uBeac.Core.Identity
{
    public partial class UserStore<TUser, TRole, TKey> : IUserLoginStore<TUser>
    {
        public async Task AddLoginAsync(TUser user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var iul = new IdentityUserLogin<string>
            {
                UserId = user.Id.ToString(),
                LoginProvider = login.LoginProvider,
                ProviderDisplayName = login.ProviderDisplayName,
                ProviderKey = login.ProviderKey
            };

            var dbUser = await _repository.GetById(user.Id, cancellationToken);
            dbUser.Logins.Add(iul);

            await _repository.Replace(dbUser, cancellationToken);
        }

        public async Task RemoveLoginAsync(TUser user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            user.Logins.RemoveAll(x => x.LoginProvider == loginProvider && x.ProviderKey == providerKey);

            var dbUser = await _repository.GetById(user.Id, cancellationToken);
            dbUser.Logins.RemoveAll(x => x.LoginProvider == loginProvider && x.ProviderKey == providerKey);

            await _repository.Replace(dbUser, cancellationToken);
        }

        public async Task<TUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(loginProvider)) throw new ArgumentNullException(nameof(loginProvider));
            if (string.IsNullOrEmpty(providerKey)) throw new ArgumentNullException(nameof(providerKey));

            cancellationToken.ThrowIfCancellationRequested();

            var users = await _repository.Find(u => u.Logins.Any(l => l.LoginProvider == loginProvider && l.ProviderKey == providerKey), cancellationToken);
            return users.SingleOrDefault();
        }

        public async Task<IdentityResult> CreateAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            cancellationToken.ThrowIfCancellationRequested();

            var exists = (await _repository.Find(x => x.UserName == user.UserName)).Any();
            if (exists) return IdentityResult.Failed(new IdentityError { Code = "Username already exists." });

            await _repository.Insert(user, cancellationToken);

            if (user.Email != null)
            {
                await SetEmailAsync(user, user.Email, cancellationToken).ConfigureAwait(false);
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            cancellationToken.ThrowIfCancellationRequested();

            await _repository.Delete(user.Id, cancellationToken);
            return IdentityResult.Success;
        }

        public async Task<TUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _repository.GetById(ConvertIdFromString(userId), cancellationToken);
        }

        public async Task<TUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return (await _repository.Find(x => x.NormalizedUserName == normalizedUserName, cancellationToken: cancellationToken)).SingleOrDefault();
        }


        public async Task<IdentityResult> UpdateAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            cancellationToken.ThrowIfCancellationRequested();

            await SetEmailAsync(user, user.Email, cancellationToken);

            await _repository.Replace(user, cancellationToken);

            return IdentityResult.Success;
        }

        public Task<string> GetNormalizedUserNameAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.NormalizedUserName ?? _normalizer.NormalizeName(user.UserName));
        }

        public Task<string> GetUserIdAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.UserName);
        }

        public async Task<IList<Claim>> GetClaimsAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var dbUser = await _repository.GetById(user.Id, cancellationToken);

            return dbUser?.Claims?.Select(x => new Claim(x.ClaimType, x.ClaimValue))?.ToList() ?? new List<Claim>();
        }

        public async Task SetNormalizedUserNameAsync(TUser user, string normalizedName, CancellationToken cancellationToken)
        {
            var name = normalizedName ?? _normalizer.NormalizeName(user.UserName);
            user.NormalizedUserName = name;
            var dbUser = await _repository.GetById(user.Id, cancellationToken);

            dbUser.NormalizedUserName = name;
            await _repository.Replace(dbUser, cancellationToken);
        }

        public async Task SetUserNameAsync(TUser user, string userName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var normalized = _normalizer.NormalizeName(userName);

            user.UserName = userName;
            user.NormalizedUserName = normalized;

            var dbUser = await _repository.GetById(user.Id, cancellationToken);

            dbUser.NormalizedUserName = normalized;
            dbUser.UserName = userName;
            await _repository.Replace(dbUser, cancellationToken);
        }


        public async Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            cancellationToken.ThrowIfCancellationRequested();

            var dbUser = await _repository.GetById(user.Id, cancellationToken);

            return dbUser?.Logins?.Select(x => new UserLoginInfo(x.LoginProvider, x.ProviderKey, x.ProviderDisplayName))?.ToList() ?? new List<UserLoginInfo>();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

    }
}
