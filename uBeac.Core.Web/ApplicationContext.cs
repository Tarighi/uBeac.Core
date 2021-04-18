using Microsoft.AspNetCore.Http;
using System;
using System.Net;

namespace uBeac.Core.Web
{
    public interface IApplicationContext<TUserKey> where TUserKey : IEquatable<TUserKey>
    {
        public TUserKey UserId { get; }
        public IPAddress UserIp { get; }
    }

    public interface IApplicationContext : IApplicationContext<Guid>
    {
    }

    public class ApplicationContext<TUserKey> : IApplicationContext<TUserKey> where TUserKey : IEquatable<TUserKey>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            UserIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress;
        }

        public TUserKey UserId { get; }
        public IPAddress UserIp { get; }

    }
    public class ApplicationContext : ApplicationContext<Guid>, IApplicationContext
    {
        public ApplicationContext(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {

        }
    }
}
