using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace uBeac.Core.Identity
{
    public partial class UserStore<TUser, TRole, TKey> : IUserTwoFactorRecoveryCodeStore<TUser>
    {
        public async Task ReplaceCodesAsync(TUser user, IEnumerable<string> recoveryCodes, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            user.RecoveryCodes = recoveryCodes.Select(x => new TwoFactorRecoveryCode { Code = x, Redeemed = false }).ToList();

            var dbUser = await _repository.GetById(user.Id, cancellationToken);
            dbUser.RecoveryCodes = user.RecoveryCodes;
            await _repository.Replace(dbUser, cancellationToken);
        }

        public async Task<bool> RedeemCodeAsync(TUser user, string code, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var dbUser = await _repository.GetById(user.Id, cancellationToken);

            if (dbUser == null) return false;

            var c = user.RecoveryCodes.FirstOrDefault(x => x.Code == code);

            if (c == null || c.Redeemed) return false;

            c.Redeemed = true;

            await _repository.Replace(dbUser, cancellationToken);

            return true;
        }

        public async Task<int> CountCodesAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var foundUser = await _repository.GetById(user.Id, cancellationToken);

            return foundUser?.RecoveryCodes?.Count ?? user.RecoveryCodes.Count;
        }
    }
}
