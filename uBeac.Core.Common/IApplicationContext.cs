using System;
using System.Net;

namespace uBeac.Core.Common
{
    public interface IApplicationContext<TUserKey> where TUserKey : IEquatable<TUserKey>
    {
        public TUserKey UserId { get; }
        public IPAddress UserIp { get; }
    }

    public interface IApplicationContext : IApplicationContext<Guid>
    {
    }
}
