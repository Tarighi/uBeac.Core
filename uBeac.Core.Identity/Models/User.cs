using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using uBeac.Core.Common;

namespace uBeac.Core.Identity
{
    public class User<TKey> : IdentityUser<TKey>, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public User()
        {
            Roles = new List<string>();
            Claims = new List<IdentityUserClaim<string>>();
            Logins = new List<IdentityUserLogin<string>>();
            Tokens = new List<IdentityUserToken<string>>();
            RecoveryCodes = new List<TwoFactorRecoveryCode>();
        }

        public User(string userName) : this()
        {
            UserName = userName;
            NormalizedUserName = userName.ToUpperInvariant();
        }

        public string AuthenticatorKey { get; set; }

        public List<string> Roles { get; set; }

        public List<IdentityUserClaim<string>> Claims { get; set; }

        public List<IdentityUserLogin<string>> Logins { get; set; }

        public List<IdentityUserToken<string>> Tokens { get; set; }

        public List<TwoFactorRecoveryCode> RecoveryCodes { get; set; }
    }

    public class User : User<Guid>, IEntity
    {
    }
}
