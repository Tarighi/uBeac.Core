using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace uBeac.Core.Identity
{
    public partial class UserStore<TUser, TRole, TKey> : IUserAuthenticationTokenStore<TUser>
    {
        public async Task SetTokenAsync(TUser user, string loginProvider, string name, string value, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user.Tokens == null) user.Tokens = new List<IdentityUserToken<string>>();

            var token = user.Tokens.FirstOrDefault(x => x.LoginProvider == loginProvider && x.Name == name);

            if (token == null)
            {
                token = new IdentityUserToken<string>
                {
                    LoginProvider = loginProvider,
                    Name = name,
                    Value = value
                };
                user.Tokens.Add(token);
            }
            else
            {
                token.Value = value;
            }

            var dbUser = await _repository.GetById(user.Id, cancellationToken);
            dbUser.Tokens = user.Tokens;

            await _repository.Replace(dbUser, cancellationToken);
        }

        public async Task RemoveTokenAsync(TUser user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var userTokens = user.Tokens ?? new List<IdentityUserToken<string>>();
            userTokens.RemoveAll(x => x.LoginProvider == loginProvider && x.Name == name);

            var dbUser = await _repository.GetById(user.Id, cancellationToken);
            var dbUserTokens = dbUser.Tokens ?? new List<IdentityUserToken<string>>();
            dbUserTokens.RemoveAll(x => x.LoginProvider == loginProvider && x.Name == name);

            await _repository.Replace(dbUser, cancellationToken);
        }

        public async Task<string> GetTokenAsync(TUser user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var token = user?.Tokens?.FirstOrDefault(x => x.LoginProvider == loginProvider && x.Name == name);

            if (token == null)
            {
                user = await _repository.GetById(user.Id, cancellationToken);
                return user?.Tokens?.FirstOrDefault(x => x.LoginProvider == loginProvider && x.Name == name)?.Value;
            }

            return token.Value;
        }
    }
}
