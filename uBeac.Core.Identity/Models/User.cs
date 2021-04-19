using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace uBeac.Core.Identity
{
    public class User<TKey> : IdentityUser<TKey>, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public string AuthenticatorKey { get; set; }
        public List<TKey> Roles { get; set; }
        public List<IdentityUserClaim<TKey>> Claims { get; set; }
        public List<IdentityUserLogin<TKey>> Logins { get; set; }
        public List<IdentityUserToken<TKey>> Tokens { get; set; }
        public List<TwoFactorRecoveryCode> RecoveryCodes { get; set; }

        public User()
        {
            Roles = new List<TKey>();
            Claims = new List<IdentityUserClaim<TKey>>();
            Logins = new List<IdentityUserLogin<TKey>>();
            Tokens = new List<IdentityUserToken<TKey>>();
            RecoveryCodes = new List<TwoFactorRecoveryCode>();
        }

        public User(string userName) : this()
        {
            UserName = userName;
        }
    }

    public class User : User<Guid>, IEntity
    {
    }
}
