using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace uBeac.Core.Identity
{
    public partial class UserStore<TUser, TRole, TKey> : IUserAuthenticationTokenStore<TUser>
    {
        public Task SetTokenAsync(TUser user, string loginProvider, string name, string value, CancellationToken cancellationToken)
        {
            if (user.Tokens == null)
                user.Tokens = new List<IdentityUserToken<TKey>>();

            var token = user.Tokens.FirstOrDefault(x => x.LoginProvider == loginProvider && x.Name == name);

            if (token == null)
            {
                token = new IdentityUserToken<TKey>
                {
                    LoginProvider = loginProvider,
                    Name = name,
                    Value = value,
                    UserId = user.Id
                };
                user.Tokens.Add(token);
            }
            else
            {
                token.Value = value;
            }
            return Task.FromResult(0);
        }

        public Task RemoveTokenAsync(TUser user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            var userTokens = user.Tokens ?? new List<IdentityUserToken<TKey>>();
            userTokens.RemoveAll(x => x.LoginProvider == loginProvider && x.Name == name);

            return Task.FromResult(0);
        }

        public Task<string> GetTokenAsync(TUser user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            var token = user?.Tokens?.SingleOrDefault(x => x.LoginProvider == loginProvider && x.Name == name);
            return Task.FromResult(token?.Value);
        }
    }
}
