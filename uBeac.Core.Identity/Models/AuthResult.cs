using System;

namespace uBeac.Core.Identity
{
    public class AuthResult<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser : User<TKey>
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public TUser User { get; set; }
    }
}
